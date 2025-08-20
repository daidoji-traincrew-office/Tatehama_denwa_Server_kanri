using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace TatehamaKanriServer.Hubs
{
    public class ConnectionHub : Hub
    {
        // 電話番号ごとのオンライン状態管理
        private static Dictionary<string, string> OnlinePhones = new(); // phoneNumber -> connectionId

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "管理サーバーに接続しました。");
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // 切断時にオンラインリストから削除
            var phone = OnlinePhones.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(phone))
            {
                OnlinePhones.Remove(phone);
                Console.WriteLine($"Phone {phone} disconnected.");
            }
            Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }

        // クライアントが自身の電話番号を登録
        public async Task RegisterPhoneNumber(string phoneNumber)
        {
            OnlinePhones[phoneNumber] = Context.ConnectionId;
            Console.WriteLine($"Phone {phoneNumber} registered as online.");
        }

        // 発信要求（from:発信元, to:着信先）
        public async Task CallRequest(string fromPhone, string toPhone)
        {
            if (OnlinePhones.ContainsKey(toPhone))
            {
                var toConnId = OnlinePhones[toPhone];
                await Clients.Client(toConnId).SendAsync("ReceiveMessage", $"ringing:{fromPhone}");
                Console.WriteLine($"Ringing: {fromPhone} -> {toPhone}");
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "no_answer");
                Console.WriteLine($"No Answer: {fromPhone} -> {toPhone}");
            }
        }
        // 着信先が受話したときに通話確立(call_ok)を通知
        public async Task AnswerCall(string fromPhone, string toPhone)
        {
            if (OnlinePhones.ContainsKey(fromPhone) && OnlinePhones.ContainsKey(toPhone))
            {
                var fromConnId = OnlinePhones[fromPhone];
                var toConnId = OnlinePhones[toPhone];
                await Clients.Client(fromConnId).SendAsync("ReceiveMessage", $"call_ok:{toPhone}");
                await Clients.Client(toConnId).SendAsync("ReceiveMessage", $"call_ok:{fromPhone}");
                Console.WriteLine($"Call OK: {fromPhone} <-> {toPhone}");
            }
            else
            {
                // 不在ならno_answer通知
                await Clients.Caller.SendAsync("ReceiveMessage", "no_answer");
                Console.WriteLine($"No Answer: {fromPhone} -> {toPhone}");
            }
        }

        // VoIPサーバー通知（未使用）
        public async Task NotifyVoipServer(string userId)
        {
            Console.WriteLine($"VoIPサーバーへの接続をユーザー '{userId}' に許可します。");
            await Clients.Caller.SendAsync("ConnectionApproved", $"ユーザー '{userId}' のVoIPサーバーへの接続が許可されました。");
        }
    }
}

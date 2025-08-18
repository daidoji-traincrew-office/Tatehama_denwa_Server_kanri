using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace TatehamaKanriServer.Hubs
{
    public class ConnectionHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            // 接続時にクライアントにメッセージを送信
            await Clients.Caller.SendAsync("ReceiveMessage", "管理サーバーに接続しました。");
            // 接続ログを出力
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // 切断ログを出力
            Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }

        // クライアントから呼び出されるメソッド
        public async Task NotifyVoipServer(string userId)
        {
            // ここでVoIPサーバーへの通知ロジックを実装
            // 今回はシミュレーションとしてログ出力
            Console.WriteLine($"VoIPサーバーへの接続をユーザー '{userId}' に許可します。");

            // 接続が許可されたことをクライアントに通知
            await Clients.Caller.SendAsync("ConnectionApproved", $"ユーザー '{userId}' のVoIPサーバーへの接続が許可されました。");
        }
    }
}

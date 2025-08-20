using System;
using System.ComponentModel.DataAnnotations;

namespace TatehamaKanriServer.Models
{
    public class PhoneSession
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = "";
        public string ConnectionId { get; set; } = "";
        public string? WorkLocation { get; set; }
        public string? DeviceType { get; set; }
        public string State { get; set; } = "Idle"; // Idle, InCall, Disconnected, etc.
        public DateTime ConnectedAt { get; set; } = DateTime.Now;
    }
}

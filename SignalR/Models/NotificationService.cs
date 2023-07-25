using Microsoft.AspNetCore.SignalR;

namespace SignalR.Models
{
    public class NotificationService : ClassePublic
    {

        public record Notification(string text, DateTime date);

        private readonly IHubContext<MySignalR> _hubContext;

        public NotificationService(IHubContext<MySignalR> hubContext)
        {
            _hubContext = hubContext;
        }   

        public Task SendNotificationAsync(Notification notification) =>
            notification is not null
            ? _hubContext.Clients.All.SendAsync("NotificationReceived", notification)
            : Task.CompletedTask;

    }
}

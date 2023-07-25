using Microsoft.AspNetCore.SignalR;

namespace SignalR.Models
{
    public class MySignalR : Hub , ClassePublic
    {
        public record Notification(string text, DateTime date);
        public Task NotifyAll(Notification notification) =>
            Clients.All.SendAsync("NotificationReceived", notification);


    }

    
}


using Microsoft.AspNetCore.SignalR.Client;
using SignalR.Models;
using System;
using System.Threading.Tasks;

namespace SignalR.Service
{
    public sealed class Consumer : IAsyncDisposable
    {
        private readonly string HostDomain =
            Environment.GetEnvironmentVariable("HOST_DOMAIN");

        private HubConnection _hubConnection;

        public Consumer()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(new Uri($"{HostDomain}/hub/notifications"))
                .WithAutomaticReconnect()
                .Build();
        }

        public Task StartNotificationConnectionAsync() =>
            _hubConnection.StartAsync();

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }
    }
    public sealed class MainClass : IAsyncDisposable
    {
        private readonly string HostDomain = Environment.GetEnvironmentVariable("");

        private MySignalR _hubConnection;

        public MainClass()
        {
            _hubConnection = new HubConnectionBuilder()
           .WithUrl(new Uri($"{HostDomain}/hub/notifications"))
           .WithAutomaticReconnect()
           .Build();
        }
        public async ValueTask DisposeAsync()
        {
           await _hubConnection.DisposeAsync();
        }
    }
}

using GameHeros.Models;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Text.Json;

namespace GameHeros.Hubs
{

    public static class GameObject
    {
        public static List<Hero> Heros = new List<Hero>()
        {
            new Naruto(),
            new Sasuke(),
        };
        public static int CurrentSkillId { get; set; } 
        public static int CurrentPlayerId { get; set; }
        public static Player Player1 { get; set; }
        public static Player Player2 { get; set; }
        public static List<Player> PlayerList { get; set; } = new List<Player>();
    }
    public class ChatHub : Hub
    {
        public Game Game { get; set; } = new Game();

        public Random random = new Random();
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChatHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public void MakeTurn(dynamic gameTO) 
        {
            var algo = gameTO.ToString().Replace("ValueKind = ", "");
            var no = JsonSerializer.Deserialize<GameTO>(algo);
        }
        
        public async Task ConnectPlayer()
        {
            AddPlayersStatic();

            await Game.Turn(this);
        }

        public async Task PlayerTurnStart(string jsonPageContent)
        {
            await Clients.All.SendAsync("PlayerTurnStart", jsonPageContent);
        }

        private void AddPlayersStatic()
        {
            var playerId = Context.ConnectionId;
            var user   = new Player();
            user.PlayerIp = playerId;
            var user2  = new Player();

            user.Hero  = GameObject.Heros[0];
            user2.Hero = GameObject.Heros[1];

            GameObject.PlayerList.Clear();
            GameObject.PlayerList.Add(user);
            GameObject.PlayerList.Add(user2);
        }

    }
}
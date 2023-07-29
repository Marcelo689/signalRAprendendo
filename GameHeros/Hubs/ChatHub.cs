using GameHeros.Models;
using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace GameHeros.Hubs
{

    public static class GameObject
    {

        public static List<Hero> Heros = new List<Hero>()
        {
            new Naruto(),
            new Sasuke(),
        };

        public static List<Player> Players = new List<Player>();
        public static int CurrentSkillId { get; set; } 
        public static Player CurrentPlayer { get; set; }
        public static Player Player1 { get; set; }
        public static Player Player2 { get; set; }
        public static List<Player> PlayerList { get; set; }
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
        public async Task MakeTurn(Game gameObject)
        {
            var idSkillUsed = gameObject.CurrentSkillId;
            gameObject.CurrentPlayer.Hero.HeroSkills.First(e => e.Id == idSkillUsed);
        }

        public async Task ConnectPlayer()
        {
            var ipDotNet = await Service.GetIp();

            AddPlayersStatic(ipDotNet);

            await Game.Turn(this);
        }

        public async Task PlayerTurnStart(string jsonPageContent)
        {
            await Clients.All.SendAsync("PlayerTurnStart", jsonPageContent);
        }

        private void AddPlayersStatic(string ipDotNet)
        {
            var user   = new Player(ipDotNet);
            var user2  = new Player(ipDotNet.Substring(0,ipDotNet.Length-2) + "9");

            user.Hero  = GameObject.Heros[0];
            user2.Hero = GameObject.Heros[1];

            GameObject.Players.Clear();
            GameObject.Players.Add(user);
            GameObject.Players.Add(user2);
        }

        public bool IpIsNew(string ip)
        {
            Player? player = GameObject.Players.FirstOrDefault(e => e.PlayerIp == ip);
            return player == null;
        }

    }
}
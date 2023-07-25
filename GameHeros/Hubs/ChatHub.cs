using GameHeros.Models;
using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace GameHeros.Hubs
{
    public class ChatHub : Hub
    {
        public Random random = new Random();
        public List<Hero> Heros = new List<Hero>()
        {
            new Charlie(),
            new X_Treme(),
        };

        public List<Player> Players = new List<Player>();
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChatHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task ConnectPlayer(string myMessage)
        {
            var ipDotNet = await Service.GetIp();

            if(IpIsNew(ipDotNet))
            {
                Player player = AddRandomHeroToUser(ipDotNet);
                Players.Add(player);
                await Clients.Caller.SendAsync("RenderPlayerPage", player);
            }

            if(Players.Count == 2) {
                GameStart();
            }

            await Clients.All.SendAsync("ReceiveMessage", myMessage, ipDotNet);
        }

        public async Task PlayerTurnStart(string jsonPageContent)
        {
            await Clients.All.SendAsync("PlayerStart", jsonPageContent);
        }

        private void GameStart()
        {
            Random orderRandomizer = new Random();
            int idP1  = orderRandomizer.Next(Players.Count);
            int idP2  = orderRandomizer.Next();

            Player firstPlayer  = Players.First(e => e.Hero?.Id == idP1);
            Player secondPlayer = Players.First(e => e.Hero?.Id == idP2);

            var inGamePlayers = new List<Player> {firstPlayer, secondPlayer};
            var idNextToPlay = firstPlayer.PlayerId;
            while(!firstPlayer.Hero.Alive || !secondPlayer.Hero.Alive)
            {
                Game.ChoosePlayer(idNextToPlay, inGamePlayers);
                Game.Turn(this);
                
            }
        }

        private Player AddRandomHeroToUser(string ipDotNet)
        {
            var user = new Player(ipDotNet);
            var heroId = random.Next(Heros.Count);

            Hero heroRandonlySelected = Heros.FirstOrDefault( e => e.Id == heroId);

            user.Hero = heroRandonlySelected;
            return user;
        }

        public bool IpIsNew(string ip)
        {
            Player? player = Players.FirstOrDefault(e => e.PlayerIp == ip);
            return player == null;
        }

    }
}
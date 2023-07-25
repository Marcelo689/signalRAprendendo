using GameHeros.Models;

namespace GameHeros.Hubs
{
    public class Player
    {
        private int IdCounter { get; set; }
        public int PlayerId { get; set; }

        public string PlayerIp;

        public Hero? Hero { get; set; }
        public Player(string ipDotNet)
        {
            IdCounter += 1;
            PlayerId = IdCounter;
            this.PlayerIp = ipDotNet;
        }
    }
}
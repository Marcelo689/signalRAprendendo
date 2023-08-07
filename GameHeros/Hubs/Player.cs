using GameHeros.Models;

namespace GameHeros.Hubs
{
    public class Player
    {
        private static int IdCounter { get; set; }
        public int PlayerId { get; set; }
        public string PlayerIp;
        public Hero? Hero { get; set; }
        public Player()
        {
            IdCounter += 1;
            PlayerId = IdCounter;
        }
    }
}
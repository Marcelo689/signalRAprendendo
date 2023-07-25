using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameHeros.Hubs
{
    internal class Game
    {
        private static Player Player { get; set; }
        public static Player ChoosePlayer(int idNextToPlay, List<Player> inGamePlayers)
        {
            Player = inGamePlayers.FirstOrDefault( e => e.PlayerId == idNextToPlay);

            return Player;
        }

        public static async void Turn(ChatHub chathub)
        {
            string jsonContent = JsonSerializer.Serialize<List<Player>>(chathub.Players);

            await chathub.PlayerTurnStart(jsonContent);
        }
    }
}
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameHeros.Hubs
{
    public class Game
    {
        public int CurrentSkillId { get; set; }
        public Player CurrentPlayer { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public List<Player> PlayerList { get; set; }
        public Player ChoosePlayer(int idNextToPlay, List<Player> inGamePlayers)
        {
            PlayerList = inGamePlayers;
            CurrentPlayer = inGamePlayers.FirstOrDefault( e => e.PlayerId == idNextToPlay);

            return CurrentPlayer;
        }

        public void ChangePlayer(){
            
            if(CurrentPlayer != Player1) {
                CurrentPlayer = Player2;
            }else 
            if(CurrentPlayer != Player2) {
                CurrentPlayer = Player1;
            }
        }
        public async Task Turn(ChatHub chathub)
        {
            var game = new Game { 
                CurrentSkillId = 0,
                CurrentPlayer = ChoosePlayer(1, GameObject.Players),
                Player1       = GameObject.Player1,
                Player2       = GameObject.Player2,
                PlayerList    = GameObject.Players,
            };

            string jsonContent = JsonSerializer.Serialize<Game>(game);

            ChangePlayer();
            await chathub.PlayerTurnStart(jsonContent);
        }
    }
}
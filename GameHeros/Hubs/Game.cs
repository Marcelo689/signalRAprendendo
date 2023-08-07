using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameHeros.Hubs
{
    public class GameTO
    {
        public GameTO()
        {
        }
        public int CurrentSkillId { get; set; }
        public int CurrentPlayerId { get; set; }
        public List<Player> PlayerList { get; set; }
        
            
        public GameTO(int currentSkillId, int currentPlayerId, List<Player> playerList)
        {
            CurrentSkillId = currentSkillId;
            CurrentPlayerId = currentPlayerId;
            PlayerList = playerList;
        }
    }

    public class Game
    {
        public int CurrentSkillId { get; set; }
        public int CurrentPlayerId { get; set; }
        public List<Player> PlayerList { get; set; }
        public int ChoosePlayer(int idNextToPlay, List<Player> inGamePlayers)
        {
            PlayerList = inGamePlayers;
            CurrentPlayerId = inGamePlayers.FirstOrDefault( e => e.PlayerId == idNextToPlay).PlayerId;

            return CurrentPlayerId;
        }

        public void ChangePlayer(){
            
            //if(CurrentPlayer != Player1) {
            //    CurrentPlayer = Player2;
            //}else 
            //if(CurrentPlayer != Player2) {
            //    CurrentPlayer = Player1;
            //}
        }
        public async Task Turn(ChatHub chathub)
        {
            var game = new Game
            { 
                CurrentSkillId = 0,
                CurrentPlayerId = ChoosePlayer(1, GameObject.PlayerList),
                PlayerList    = GameObject.PlayerList,
            };

            string jsonContent = JsonSerializer.Serialize<Game>(game);

            ChangePlayer();
            await chathub.PlayerTurnStart(jsonContent);
        }
    }
}
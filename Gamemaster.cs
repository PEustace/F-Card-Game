//A script that holds the brain of the game serving both players.
//It also holds an additional class for default values.
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Game {
    public class Gamemaster {
        //PRIVATE
        //
        //
        //
        //
        private List<IEffect> EffectsList = new();
        //Initialize players
        public Dictionary<string, PlayerData> playerList = new();

        private void InitPlayerList() {
            PlayerData player1 = new PlayerData(defaultTP, defaultMP, defaultEC, defaultCS, "Player 1");
            PlayerData player2 = new PlayerData(defaultTP, defaultMP, defaultEC, defaultCS, "Player 2");

            playerList.Add(player1.GetName(), player1);
            playerList.Add(player2.GetName(), player2);
        }
        //PUBLIC
        //
        //
        //
        //
        //Game start values
        public const int defaultTP = 6;
        public const int defaultMP = 5;
        public const int defaultEC = 0;
        public const int defaultCS = 0;
        public const int defaultDrawCount = 5;
        public static Dictionary<string, bool> defaultPlayerFlags = new Dictionary<string, bool>() {
            {"canSurvey", false}
        };
        public static Dictionary<string, bool> defaultCardFlags = new Dictionary<string, bool>() {
            {"expiresInBreak", false},
            {"cardRevealed", false}
        };
        public Gamemaster() {
            InitPlayerList();
            Console.WriteLine("Deck created.");
            playerList["Player 1"].GetHand().TestDeck();
            Console.WriteLine("Tested deck successfully.");
        }
        public PlayerData GetPlayerObject(string playerName) {
            return playerList[playerName];
        }
        //notEnemyName means the current, first-person player's name
        public PlayerData GetEnemyObject(string notEnemyName) {
            foreach (string player in playerList.Keys) {
                if (notEnemyName != player) {
                    return playerList[player];
                }
            }
            return new PlayerData(0, 0, 0, 0, "Error Dummy.");
        }

        public static void Main() {
            Gamemaster gamemaster = new();
        }
    }
}
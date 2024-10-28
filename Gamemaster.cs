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
            {"canSurvey", true}
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

            //Begin the game proper
            Gameplay();
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
        public void CastPhase(PlayerData activePlayer) {
            bool turnActive = true;
            //The condition that the turn is good to go
            bool turnOkay = true;
            List<Card> playerHand = activePlayer.GetHand().GetContents();
            foreach (string flag in activePlayer.GetFlags().Keys) {
                Console.WriteLine("Flags found.");
                CheckTurnFlag(flag, activePlayer);
            }
            if (turnOkay == true) {
                Console.WriteLine("Turn Active: " + activePlayer.GetName() + ".");
                Console.WriteLine("Cards: ");
                for (int i = 0; i < playerHand.Count; i++) {
                    Console.WriteLine(i + ": " + playerHand[i].GetName());
                }
                Console.Write("Play Card...: ");
                while (turnActive == true) {
                    //Check for the card choice
                    try {
                        int choice = Convert.ToInt32(Console.ReadLine());
                        Card cardChoice = playerHand[choice];
                        Console.WriteLine("Card has taken the field: " + cardChoice.GetName());
                        //Now we want to apply the effects of the card
                        foreach (IEffect cardEffect in cardChoice.Effects) {
                            Console.WriteLine("Applying Effect: " + cardEffect.GetName());
                            if (cardEffect is ChangeFlag changeFlag) {
                                changeFlag.Apply(activePlayer);
                            }
                        }
                        turnActive = false;
                    }
                    catch {
                        Console.WriteLine("Invalid card number.");
                    }
                }
            }
            Console.WriteLine("End of turn. Starting next player turn.");
        }
        public void CheckTurnFlag(string flag, PlayerData activePlayer) {
            
        }
        public void Gameplay() {
            PlayerData player1 = playerList["Player 1"];
            PlayerData player2 = playerList["Player 2"];
            Console.WriteLine("Begin Turns.");
            //while (true) {
                CastPhase(playerList["Player 1"]);
                CastPhase(playerList["Player 2"]);
            //}
        }
    }
}
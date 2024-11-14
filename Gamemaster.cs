//A script that holds the brain of the game serving both players.
//It also holds an additional class for default values.
using System.Diagnostics;
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
            PlayerData player1 = new PlayerData(defaultTP, defaultMP, defaultEC, defaultCS, defaultHP, "Player 1");
            PlayerData player2 = new PlayerData(defaultTP, defaultMP, defaultEC, defaultCS, defaultHP, "Player 2");

            playerList.Add(player1.GetName(), player1);
            playerList.Add(player2.GetName(), player2);
        }
        Phases ShouldPhaseEnd(Phases currentPhase) {

            //Underneath we check all the conditions for both phases
            //Reference "Phases" in the rulebook for current phase conditions

            //Check for each player with the endconditions based on phase
            if (currentPhase == Phases.Cast) {
                foreach (string playerKey in playerList.Keys) {
                    PlayerData player = playerList[playerKey];
                    bool[] endCastConditions = [
                        player.GetTP() <= 0,
                        player.GetHand().GetActiveServant() != null
                    ];
                    if (endCastConditions.Any(condition => condition)) {
                        return Phases.Break;
                    }
                    else {
                        return currentPhase;
                    }
                }
            }
            else if (currentPhase == Phases.Break) {
                foreach (string playerKey in playerList.Keys) {
                    PlayerData player = playerList[playerKey];
                    bool[] endBreakConditions = [
                        player.GetHealth() <= 0
                    ];

                    if (endBreakConditions.Any(condition => condition)) {
                        return Phases.End;
                    }
                    else {
                        return currentPhase;
                    }
                }
            }
            else {
                Console.WriteLine("oopsie woopsie. this shouldn't happen.");
                return Phases.End;
            }
            //somehow
            return currentPhase;
            

            
        }
        //PUBLIC
        //
        //
        //
        //
        //Game start values
        //Set the initial phase, which is castphase
        public enum Phases {Cast, Break, End}
        public const int defaultTP = 6;
        public const int defaultMP = 5;
        public const int defaultEC = 0;
        public const int defaultCS = 0;
        public const int defaultHP = 5;
        public const int defaultDrawCount = 10;
        public const int defaultServantCount = 5;
        public const Phases defaultPhase = Phases.Cast;
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
            return new PlayerData(0, 0, 0, 0, 0, "Error Dummy.");
        }

        public static void Main() {
            Gamemaster gamemaster = new();
        }
        public void CastPhase(PlayerData activePlayer, PlayerData enemyPlayer) {
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
                        RequestPlayCard(activePlayer, playerHand, enemyPlayer);
                        turnActive = false;
                    }
                    catch {
                        Console.WriteLine("Invalid card number.");
                    }
                }
            }
            Console.WriteLine("End of turn. Starting next player turn.");
        }
        //See rulebook for specifics of break phase functionality.
        public void BreakPhase(PlayerData activePlayer, PlayerData enemyPlayer) {
            PrepareBreak();

            Console.WriteLine("Your active servant is: ");

            Console.WriteLine(activePlayer.GetHand().GetActiveServant().GetName());

            Console.WriteLine("Which action would you like to take? \n 1: Servant Action \n 2: Play a Card.");
            try {
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 0) {
                    RequestServantAction(activePlayer, enemyPlayer);
                }
                else if (choice == 1) {
                    RequestPlayCard(activePlayer, activePlayer.GetHand().GetContents(), enemyPlayer);
                }
                else {
                    Console.WriteLine("Invalid choice.");
                }
            }
            catch {
                Console.WriteLine("Invalid choice.");
            }
        }
        public void CheckTurnFlag(string flag, PlayerData activePlayer) {
            
        }
        public void PrepareBreak() {
            foreach (string player in playerList.Keys) {
                if (playerList[player].GetHand().GetActiveServant() == null) {
                    playerList[player].GetHand().SetRandomServant();
                }
            }
        }
        public void RequestServantAction(PlayerData activePlayer, PlayerData enemyPlayer) {
            int choiceAction = Convert.ToInt32(Console.ReadLine());
        }
        public void RequestPlayCard(PlayerData activePlayer, List<Card> playerHand, PlayerData enemyPlayer) {
            int choice = Convert.ToInt32(Console.ReadLine());
            Card cardChoice = playerHand[choice];
            //Deduct costs from the card
            foreach (string valueKey in cardChoice.Cost.Keys) {
                activePlayer.DecreaseValue(cardChoice.Cost[valueKey], valueKey);
            }
            Console.WriteLine("Card has taken the field: " + cardChoice.GetName());
            List<string> messagesStrings = new();
            //Now we want to apply the effects of the card
            foreach (IEffect cardEffect in cardChoice.Effects) {
                Console.WriteLine("It's a(n): " + cardEffect.GetWhich());
                if (cardEffect.GetWhich() == "player" || cardEffect.GetWhich() == "enemy") {
                    messagesStrings = cardEffect.Apply(activePlayer, enemyPlayer);
                }
                else if (cardEffect.GetWhich() == "playerCard" || cardEffect.GetWhich() == "enemyCard") {
                    //Check the effect for what card(s) it wants
                    //Then pass that into the effect apply
                    if (cardEffect.GetCategory() == "name") {
                        Card returnedType = cardEffect.GetCardOfName(activePlayer, enemyPlayer);
                        messagesStrings = cardEffect.Apply(returnedType);
                    } 
                    else if (cardEffect.GetCategory() == "faction") {
                        List<Card> returnedType = cardEffect.GetCardsOfFaction(activePlayer, enemyPlayer);
                        messagesStrings = cardEffect.Apply(returnedType);
                    }
                }
                Console.WriteLine("Applying Effect: " + cardEffect.Name);
                
                //Writing out visual feedback for the player;
                foreach(string message in messagesStrings) {
                        Console.WriteLine(message);
                }
            }
        }
        public void Gameplay() {
            PlayerData player1 = playerList["Player 1"];
            PlayerData player2 = playerList["Player 2"];
            Console.WriteLine("Begin Turns.");

            Phases currentPhase = defaultPhase;

            while (currentPhase == Phases.Cast || currentPhase == Phases.Break) {
                //Why separate? I want to avoid situations where a player starts
                //another phase, but someone gets a second duplicate turn in the
                //existing phase first.

                //Check and play first player
                switch (ShouldPhaseEnd(currentPhase)) {
                    case Phases.Cast: 
                        Console.WriteLine("Tension is in the air...");
                        currentPhase = Phases.Cast;
                        CastPhase(player1, player2);
                    break;
                   case Phases.Break: 
                        Console.WriteLine("TENSION BROKEN");
                        currentPhase = Phases.Break;
                        BreakPhase(player1, player2);
                    break;
                }
                //Check and play second player
                switch (ShouldPhaseEnd(currentPhase)) {
                    case Phases.Cast: 
                        Console.WriteLine("Tension is in the air...");
                        currentPhase = Phases.Cast;
                        CastPhase(player2, player1); 
                    break;
                   case Phases.Break: 
                        Console.WriteLine("TENSION BROKEN");
                        currentPhase = Phases.Break;
                        BreakPhase(player2, player1);
                    break;
                }
            }
            Console.WriteLine("Game over.");
        }
    }
}
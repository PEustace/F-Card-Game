using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Game {
    public interface IEffect {
        //PRIVATE
        //
        //
        //
        
        //PUBLIC
        //
        //
        //
        public string Name {get; set;}
        public string which {get; set;}
        public string effectMessage {get; set;}
        public virtual List<string> Apply() {
            return ["Does not apply"];
        }
        public virtual List<string> Apply(PlayerData player, PlayerData enemy) {
            return ["Does not apply to players."];
        }
        public virtual string GetWhich() {
            return which;
        }
        public virtual object GetCardNameOrFaction(PlayerData player, PlayerData enemy) {
            return null;
        }
        public virtual List<string> Apply(Card card) {
            return ["Found no card to apply."];
        }
        public virtual List<string> Apply(List<Card> cards) {
            return ["Found no list of cards to apply."];
        }
        public List<string> Apply(PlayerData player) {
            return ["Found no player to apply"];
        }
    }

    //Card games in OOP are tricky.
    //We need functions to handle different groups of things.
    //But we can't just make a class for every card (nor do I want to as the game data is stored externally in JSON)
    //Solution is to have effects run by checking a list of effect names on the cards.
    //Subject to change.
    public class ChangePlayerValue : IEffect {
        public string Name {get; set;} = "Change Player Value";
        public Dictionary<string, bool> newPlayerValue {get; set;} = new();
        public string which { get; set; } = "Default Value.";
        public string effectMessage {get; set;}
        //Strings that provide context to the players about what happened
        public List<string> returnStrings = new();
        public string Apply() {
           return "Apply ran, but it did not apply any effect.";
        }
        public List<string> Apply(PlayerData player, PlayerData enemy) {
            switch (which) {
                case "player":
                    foreach (string valueName in newPlayerValue.Keys) {
                        player.ChangeFlag(valueName, newPlayerValue[valueName]);
                        returnStrings.Add("Player '" + player.GetName() + "' is now " + effectMessage);
                    }
                    break;
                case "enemy":
                    foreach (string valueName in newPlayerValue.Keys) {
                        enemy.ChangeFlag(valueName, newPlayerValue[valueName]);
                        returnStrings.Add("Player '" + enemy.GetName() + "' is now " + effectMessage);
                    }
                    break;
                case "all":
                    break;
            }
            return returnStrings;
        }
    }
    public class ChangeCardValue : IEffect {
        public string effectMessage {get; set;}
        public string Name {get; set;} = "Change Card Value";
        public Dictionary<string, bool> newCardValue {get; set;} = new();
        public string which { get; set; }
        public List<string> Apply() {
            return ["Not set up yet."];
        }
    }
    public class ApplyDamage : IEffect {
        public string effectMessage {get; set;}
        public string Name {get; set;} = "Apply Damage";
        public int damageCount {get; set;}
        public string applyToName {get; set;}
        public string applyCategory {get; set;}
        public string which {get; set;}
        //It's necessary to understand whether the apply needs to apply to a card or a list of cards, or other.
        public object GetDamageNameOrFaction<T>(PlayerData activePlayer, PlayerData enemyPlayer) {
            List<Card> pullFromHand = new();
            List<Card> filteredList = new();
            //faction returns a list of cards matching that faction
            if (applyCategory == "faction") {
                if (which == "playerCard") {
                    pullFromHand = activePlayer.GetHand().GetContents();
                }
                else if (which == "enemyCard") {
                    pullFromHand = enemyPlayer.GetHand().GetContents();
                }

                foreach (Card card in pullFromHand) {
                    //if it doesn't match the intended target, just cut it from the list
                    if (card.Faction != applyToName) {
                        filteredList.Add(card);
                    }
                }

                return filteredList;
            }
            //name returns a singular card of that name
            else if (applyCategory == "name") {
                if (which == "playerCard") {
                    pullFromHand = activePlayer.GetHand().GetContents();
                }
                else if (which == "enemyCard") {
                    pullFromHand = enemyPlayer.GetHand().GetContents();
                }
                
                foreach (Card card in pullFromHand) {
                    if (card.Name == applyToName) {
                        return card;
                    }
                }
            }
            else if (applyCategory == "player") {
                PlayerData applyToPlayer;
                if (which == "player") {
                    applyToPlayer = activePlayer;
                }
                else if (which == "enemy") {
                    applyToPlayer = enemyPlayer;
                }
                else {
                    applyToPlayer = null;
                }
                return applyToPlayer;
            }
            //this shouldn't happen
            Console.WriteLine("Goof in aisle GCNOF() on IEffect");
            return null;
        }
        public List<string> Apply(Card card) {
            card.TakeDamage(damageCount);
            return [card.Name + " has had " + damageCount.ToString() + " " + effectMessage];
        }
        public List<string> Apply(List<Card> cards) {
            List<string> returnStrings = new();
            foreach (Card card in cards) {
                card.TakeDamage(damageCount);
                returnStrings.Add(card.Name + " has had " + damageCount.ToString() + " " + effectMessage);
            }
            return returnStrings;
        }
        public List<string> Apply(PlayerData player) {
            player.TakeDamage(damageCount);
            return [player.GetName() + " has had " + damageCount.ToString() + " " + effectMessage];
        }
    }
}
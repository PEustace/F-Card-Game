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
            }
            return returnStrings;
        }
    }
    public class ChangeCardValue : IEffect {
        public string effectMessage {get; set;}
        public string Name {get; set;} = "Change Card Value";
        public Dictionary<string, bool> newCardValue {get; set;} = new();
        public string which { get; set; }
        public void Apply() {

        }
    }
    public class ApplyDamage : IEffect {
        public string effectMessage {get; set;}
        public string Name {get; set;} = "Apply Damage";
        public string damageCount {get; set;}
        public string applyToType {get; set;}
        public string which {get; set;}
        public void Apply() {

        }
    }
}
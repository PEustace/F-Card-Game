using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Game {
    public interface IEffect {
        //PRIVATE
        //
        //
        //
        private static string name;
        //PUBLIC
        //
        //
        //
        public void Apply();
        public string GetName() {
            return name;
        }
    }

    //Card games in OOP are tricky.
    //We need functions to handle different groups of things.
    //But we can't just make a class for every card (nor do I want to as the game data is stored externally in JSON)
    //Solution is to have effects run by checking a list of effect names on the cards.
    //Subject to change.
    public class ChangePlayerValue : IEffect {
        string name = "Change Player Value";
        public Dictionary<string, bool> newPlayerValue {get; set;} = new();
        public void Apply() {

        }
    }
    public class ChangeCardValue : IEffect {
        string name = "Change Card Value";
        public Dictionary<string, bool> newCardValue {get; set;} = new();
        public void Apply() {

        }
    }
    public class ApplyDamage : IEffect {
        string name = "Apply Damage";
        public string damageCount {get; set;}
        public string applyToType {get; set;}
        public void Apply() {

        }
    }
}
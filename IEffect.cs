using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;

namespace Game {
    public interface IEffect {
        //PRIVATE
        //
        //
        //
        public string effectName {get; set;}
        //PUBLIC
        //
        //
        //
        public void Apply() {

        }
        public string GetName() {
            return effectName;
        }
    }
    //Card games in OOP are tricky.
    //We need functions to handle different groups of things.
    //But we can't just make a class for every card (nor do I want to as the game data is stored externally in JSON)
    //Solution is to have effects run by checking a list of effect names on the cards.
    //Subject to change.
    public class ChangeFlag : IEffect {
        string flag;
        bool value;
        public string effectName {get; set;} = "ChangeFlag";
        public ChangeFlag(string flagToChange, bool newFlagValue) {
            flag = flagToChange;
            value = newFlagValue;
        }
        public void Apply(PlayerData playerToApply) {
            playerToApply.ChangeFlag(flag, value);
            
        }
        public void Apply(Card cardToApply) {
            cardToApply.ChangeFlag(flag, value);
        }
    }
}
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;

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
        public void Apply() {

        }
    }

    //Card games in OOP are tricky.
    //We need functions to handle different groups of things.
    //But we can't just make a class for every card (nor do I want to as the game data is stored externally in JSON)
    //Solution is to have effects run by checking a list of effect names on the cards.
    public class ChangeFlag : IEffect {
        public void Apply(PlayerData playerToApply, string flagToChange, bool newFlagValue) {
            playerToApply.ChangeFlag(flagToChange, newFlagValue);
            
        }
        public void Apply(Card cardToApply, string flagToChange, bool newFlagValue) {
            cardToApply.ChangeFlag(flagToChange, newFlagValue);
        }
    }
}
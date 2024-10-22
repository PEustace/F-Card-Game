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
        public void EffectAction() {

        }
    }

    public class ChangeEnemyAttribute : Gamemaster, IEffect {
        public void EffectAction() {

        }
        public ChangeEnemyAttribute(string flagToChange, bool newFlagValue) {
            GetEnemyObject("Player 2").ChangeFlag(flagToChange, newFlagValue);
        }
    }
    public class ChangeCardAttribute : Gamemaster, IEffect {
        public void EffectAction() {

        }
        public ChangeCardAttribute(string flagToChange, bool newFlagValue) {
            
        }
    }
}
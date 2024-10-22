using System.Text.Json.Nodes;

namespace Game {
    public interface IEffect {
        //PRIVATE
        //
        //
        //
        //A list of all flags in the game with default values
        private void EffectAction() {}
        //PUBLIC
        //
        //
        //
    }

    public class ChangeEnemyAttribute : IEffect {
        private void EffectAction() {
            
        }
        public ChangeEnemyAttribute(string flagToChange, bool newFlagValue) {
            PlayerData.ChangeFlag(flagToChange, newFlagValue);
        }
    }
}
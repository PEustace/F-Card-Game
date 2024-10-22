//A class containing functions and data relating to an individual player.

namespace Game {
    public class PlayerData {
        //PRIVATE
        //
        //
        //
        //The player hand of cards
        Hand hand = new Hand();
        int tp;
        int mp;
        int ec; //exposure counters
        int cs; //command counters/seals

        string name;

        //Private flags (i.e. if they can play survey or if they are still hidden)
        Dictionary<string, bool> flags = new();

        private void PlayerInit(int tpVal, int mpVal, int ecVal, int csVal, string nameVal) {
            tp = tpVal;
            mp = mpVal;
            ec = ecVal;
            cs = csVal;
            name = nameVal;
        }

        private void InitFlags() {
            flags.Add("canSurvey", true);
        }
        //PUBLIC
        //
        //
        //
        public PlayerData(int tacticPoints, int manaPoints, int exposureCounters, int commandCounters, string playerName) {
            PlayerInit(tacticPoints, manaPoints, exposureCounters, commandCounters, playerName);

            InitFlags();
        }

        public void ChangeFlag(string flagName, bool newFlagValue) {
            flags[flagName] = newFlagValue;
        }
        public void IncreaseValue(int change, string attribute) {
            switch (attribute) {
                case "tp": tp += change; break;
                case "mp": mp += change; break;
                case "ec": ec += change; break;
                case "cs": cs += change; break;
            }
        }
        public void DecreaseValue(int change, string attribute) {
            switch (attribute) {
                case "tp": tp -= change; break;
                case "mp": mp -= change; break;
                case "ec": ec -= change; break;
                case "cs": cs -= change; break;
            }
        }
    }
}

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

        int hp;
        string name;
        int playerNumber;
        //Private flags (i.e. if they can play survey or if they are still hidden)
        Dictionary<string, bool> flags;

        private void PlayerInit(int tpVal, int mpVal, int ecVal, int csVal, int hpVal, string nameVal) {
            tp = tpVal;
            mp = mpVal;
            ec = ecVal;
            cs = csVal;
            hp = hpVal;
            name = nameVal;
        }

        private void InitFlags() {
            flags = Gamemaster.defaultPlayerFlags;
        }
        //PUBLIC
        //
        //
        //
        public PlayerData(int tacticPoints, int manaPoints, int exposureCounters, int commandCounters, int healthPoints, string playerName) {
            PlayerInit(tacticPoints, manaPoints, exposureCounters, commandCounters, healthPoints, playerName);

            InitFlags();
        }

        public string GetName() {
            return name;
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

        public Hand GetHand() {
            return hand;
        }
        public Dictionary<string, bool> GetFlags() {
            return flags;
        }
        public void TakeDamage(int damageCount) {
            hp -= damageCount;
        }
    }
}

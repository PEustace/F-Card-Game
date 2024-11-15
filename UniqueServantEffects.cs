//All servant effects are unique.
//They can still be mixed and matched in the event of a character needing to copy them.
//But we will define these very specifically as opposed to card effects.
//Do note servants can still have card effects as passive effects.

namespace Game {
    public interface IUnique {
        public string Name {get; set;}
        public string Body {get; set;}

        public virtual void ApplyUnique() {
            Console.WriteLine("Not set");
        } 
        public virtual void ApplyUnique(PlayerData activePlayer, PlayerData enemyPlayer) {
            Console.WriteLine("Not set");
        } 
    }
    class Excalibur : IUnique {
        public string Name {get; set;} = "Excalibur";
        public string Body {get; set;} = "Sword given by the Lady of the Lake.";

        public void ApplyUnique(PlayerData activePlayer, PlayerData enemyPlayer) {
            int mpCount = activePlayer.GetMP();
            int damageDealt = 0;
            activePlayer.DecreaseValue(mpCount, "mp");

            if (mpCount >= 5) {
                mpCount -= 5;
                damageDealt = 5;

                if (mpCount > 0) {
                    for (int i = 1; i <= mpCount; i++) {
                        damageDealt += 2;
                    }
                }
            }
            else {
                damageDealt = 0;
                Console.WriteLine("No Mana Available");
            }

            enemyPlayer.GetHand().GetActiveServant().TakeDamage(damageDealt);
        }

    }
    class InvisibleAir : IUnique {
        public string Name {get; set;} = "Invisible Air";
        public string Body {get; set;} = "A mighty wind obscures the sword from view.";

        public void ApplyUnique(PlayerData activePlayer, PlayerData enemyPlayer) {
            int mpCount = activePlayer.GetMP();
            
            if (mpCount > 0) {
                activePlayer.DecreaseValue(1, "mp");
                enemyPlayer.GetHand().GetActiveServant().TakeDamage(1);
            }
            else if (mpCount <= 0) {
                Console.WriteLine("No mana available.");
            }
        }
    }
    class GaeBolg : IUnique {
        public string Name {get; set;} = "Gae Bolg";
        public string Body {get; set;} = "A weapon that will always pierce the heart.";

        public void ApplyUnique(PlayerData activePlayer, PlayerData enemyPlayer) {
            int mpCount = activePlayer.GetMP();
            
            if (mpCount > 0) {
                activePlayer.DecreaseValue(1, "mp");
                enemyPlayer.GetHand().GetActiveServant().TakeDamage(1);
            }
            else if (mpCount <= 0) {
                Console.WriteLine("No mana available.");
            }
        }
    }
    class RuneMagic : IUnique {
        public string Name {get; set;} = "Rune Magic";
        public string Body {get; set;} = "Affinity with runes.";
        public void ApplyUnique(PlayerData activePlayer, PlayerData enemyPlayer) {
            int mpCount = activePlayer.GetMP();
            
            if (mpCount > 0) {
                Console.WriteLine("Consume how much MP?: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                activePlayer.DecreaseValue(choice, "mp");
                if (choice > 5) {
                    choice = 5;
                }

                //Refund?
                bool refund = RunChance();
                if (refund && choice <= 3) {
                    activePlayer.IncreaseValue(1, "mp");
                }
                enemyPlayer.GetHand().GetActiveServant().TakeDamage(choice);
            }
            else if (mpCount <= 0) {
                Console.WriteLine("No mana available.");
            }
        }
        bool RunChance() {
            Random rand = new Random();
            int randInt = rand.Next(0, 3);
            switch (randInt) {
                case 0: return false;
                case 1: return false;
                case 2: return true;
            }
            return false;
        }
    }
}
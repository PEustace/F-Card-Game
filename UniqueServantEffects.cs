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
    }
    class Excalibur : IUnique {
        public string Name {get; set;} = "Excalibur";
        public string Body {get; set;} = "Sword of the fairies.";

        public void ApplyUnique(PlayerData activePlayer, PlayerData enemyPlayer) {
            int mpCount = activePlayer.GetMP();
            int damageDealt = 0;
            activePlayer.DecreaseValue(mpCount, "mp");

            if (mpCount >= 5) {
                mpCount -= 5;
                damageDealt = 5;

                if (mpCount > 0) {
                    for (int i = 0; i < mpCount; i++) {
                        damageDealt += 2;
                    }
                }
            }
            else {
                damageDealt = 0;
                Console.WriteLine("No Mana Available");
            }

            activePlayer.GetHand().GetActiveServant().TakeDamage(damageDealt);
        }

    }
}
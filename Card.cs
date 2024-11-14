namespace Game {
    public class Card {
        //Enum types of the card
        private enum EnumType {Card, Servant}
        public bool Visible {get; set;}
        public string Name {get; set;}
        public string Type {get; set;}
        private EnumType internalType;
        public string Body {get; set;}
        public string Faction {get; set;} = "none";
        public int Health {get; set;} = 1;
        public Dictionary<string, int> Cost {get; set;}
        public Dictionary<string, bool> flags = new();
        public List<IEffect> Effects {get; set;}
    
        //PUBLIC
        //
        //
        //

        //public Card() {
            //TagEffectsToCard();
        //}
        public string GetName() {
            return Name;
        }
        public void ChangeVisibility(bool visibility) {
            Visible = visibility;
        }
        public void ChangeFlag(string flagToChange, bool newFlagValue) {

        }
        public void TakeDamage(int damageCount) {
            Console.WriteLine("Owie!");
            Health -= damageCount;
        }
        //This function handles the presenting of the card to the playspace, but effects
        //are handled elsewhere
        public void PlayCard(List<Card> cardsInPlay) {
        }
    }
    //Servants have a few extra rules to normal cards. See rulebook.
    public class Servant : Card {

    }
}
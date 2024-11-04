namespace Game {
    public class Card {
        public bool Visible {get; set;}
        public string Name {get; set;}
        public string Type {get; set;}
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
    }
}
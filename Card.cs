namespace Game {
    public class Card {
        //PRIVATE
        //
        //
        //
        bool Visible {get; set;}
        string Name {get; set;}
        string Type {get; set;}
        string Body {get; set;}
        Dictionary<string, int> Cost {get; set;} = new();
        Effects Effects {get; set;} = new();
        //PUBLIC
        //
        //
        //
        public Card(bool isVisible, string cardName, string typeName, string cardBody, Dictionary<string, int> cardCost, Effects cardEffects) {
            Visible = isVisible;
            Name = cardName;
            Type = typeName;
            Body = cardBody;
            Cost = cardCost;
            Effects = cardEffects;
        }

        public string GetName() {
            return Name;
        }
        public void ChangeVisibility(bool visibility) {
            Visible = visibility;
        }
    }
}
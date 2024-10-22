namespace Game {
    public class Card {
        //PRIVATE
        //
        //
        //
        public bool Visible {get; set;}
        public string Name {get; set;}
        public string Type {get; set;}
        public string Body {get; set;}
        public Dictionary<string, int> Cost {get; set;} = new();
        //We have a list to hold the strings on the card and a list to hold the actual effects since we're bringing in from JSON.
        public string[] EffectsList {get; set;}

        public void TagEffectsToCard() {
            foreach (string effect in EffectsList) {
                switch (effect) {
                    case "blockEnemySurvey": EffectsList.Append(new ChangeEnemyAttribute("canSurvey", false)); break;
                    case "cardExpiresInBreak": Console.WriteLine(" | Set to Expire"); break;
                    case "revealEnemyCards": Console.WriteLine(" | Cards Revealed"); break;
                }
                
            }
        }
        //PUBLIC
        //
        //
        //
        //public Card(bool isVisible, string cardName, string typeName, string cardBody, Dictionary<string, int> cardCost, string[] cardEffects) {
        //    Visible = isVisible;
        //    Name = cardName;
        //    Type = typeName;
        //    Body = cardBody;
        //    Cost = cardCost;
        //    EffectsList = cardEffects;
        //}

        //public Card() {
            //TagEffectsToCard();
        //}
        public string GetName() {
            return Name;
        }
        public void ChangeVisibility(bool visibility) {
            Visible = visibility;
        }
        
    }
}
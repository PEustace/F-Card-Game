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
        //We actually need two effects lists.
        //One of them holds the strings to tell the card what effects it has (from JSON)
        //The other holds the actual effect objects and is populated inside the card.
        public string[] EffectsStringList {get; set;}
        private List<IEffect> EffectsList = new();
        //A method to search through all effects in the card's parameters
        //and append the necessary method to act
        public void TagEffectsToCard() {
            if (EffectsStringList == null) {
                Console.WriteLine("EffectsStringList is null. Cannot tag effects.");
                return;
            }
            foreach (string effect in EffectsStringList) {
                switch (effect) {
                    case "blockEnemySurvey": 
                        EffectsList.Add(new ChangeEnemyAttribute("canSurvey", false));
                        Console.WriteLine(EffectsList);
                        break;
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
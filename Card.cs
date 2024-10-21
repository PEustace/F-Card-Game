using System.Runtime.InteropServices;

namespace Game {
    public class Card {
        //PRIVATE
        //
        //
        //
        bool visible;
        string name;
        string type;
        string body;
        Dictionary<string, int> cost = new();
        Effects effects;
        //PUBLIC
        //
        //
        //
        public Card(bool isVisible, string cardName, string typeName, string cardBody, Dictionary<string, int> cardCost, Effects cardEffects) {
            visible = isVisible;
            name = cardName;
            type = typeName;
            body = cardBody;
            cost = cardCost;
            effects = cardEffects;
        }

        public void ChangeVisibility(bool visibility) {
            visible = visibility;
        }
    }
}
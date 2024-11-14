using System.ComponentModel;

namespace Game {
    public class Hand : Deck {
        //PRIVATE
        //
        //
        //
        List<Card> handContents = new List<Card>();
        List<Card> servantContents = new List<Card>();
        Card activeServant;

        //PUBLIC
        //
        //
        //
        public Hand() {
            handContents = DrawCards(Gamemaster.defaultDrawCount);
            servantContents = DrawServants(Gamemaster.defaultServantCount);
            //foreach (Card card in handContents) {
            //    Console.WriteLine("Beginning Hand construction: " + card.GetName());
            //}
            
            foreach (Card card in handContents) {
                //Console.WriteLine("My hand has: " + card.GetName());
            }
        }
        public List<Card> GetContents() {
            return handContents;
        }
        public List<Card> GetServants() {
            return servantContents;
        }
        public void SetRandomServant() {
            Random rand = new Random();

            int randInt = rand.Next(0, servantContents.Count);
            Card chosenServant = servantContents[randInt];

            activeServant = chosenServant;
        }
        public Card GetActiveServant() {
            return activeServant;
        }
    }
}
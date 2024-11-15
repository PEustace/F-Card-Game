using System.ComponentModel;

namespace Game {
    public class Hand : Deck {
        //PRIVATE
        //
        //
        //
        List<Card> handContents = new List<Card>();
        List<Servant> servantContents = new List<Servant>();
        Servant activeServant;

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
        public List<Servant> GetServants() {
            return servantContents;
        }
        public void SetRandomServant() {
            Random rand = new Random();

            int randInt = rand.Next(0, servantContents.Count);
            Servant chosenServant = servantContents[randInt];

            activeServant = chosenServant;
        }
        public Servant GetActiveServant() {
            return activeServant;
        }
    }
}
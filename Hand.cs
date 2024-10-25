using System.ComponentModel;

namespace Game {
    public class Hand : Deck {
        //PRIVATE
        //
        //
        //
        List<Card> handContents = new List<Card>();

        //PUBLIC
        //
        //
        //
        public Hand() {
            handContents = DrawCards(Gamemaster.defaultDrawCount);
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
    }
}
using System.ComponentModel;

namespace Game {
    public class Hand : Deck {
        //PRIVATE
        //
        //
        //
        List<Card> handContents = new();

        //PUBLIC
        //
        //
        //
        public Hand() {
            handContents = DrawCards(Gamemaster.defaultDrawCount);
            Console.WriteLine("Beginning Hand construction: " + handContents);
            foreach (Card card in handContents) {
                //Console.WriteLine("My hand has: " + card.GetName());
            }
        }
    }
}
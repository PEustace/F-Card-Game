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
            Console.WriteLine("Beginning Hand construction.");
            handContents = DrawCards(Gamemaster.defaultDrawCount);
            foreach (Card card in handContents) {
                Console.WriteLine("My hand has: " + card.GetName());
            }
        }
    }
}
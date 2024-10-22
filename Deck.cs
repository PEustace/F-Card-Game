using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Game {
    public class Deck {
        //PRIVATE
        //
        //
        //
        List<Card> deck;
        private Card dummyCard;
        
        private void CreateDeck() {
            string filePath = "./cardlist.json";
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(filePath);

            // Deserialize the JSON string into a list of Card objects
            deck = JsonSerializer.Deserialize<List<Card>>(jsonData);

            //Create a "Burnout" card that acts as a dummy card in case
            //there are errors with the deck.
            filePath = "./dummycard.json";
            jsonData = File.ReadAllText(filePath);
            dummyCard = JsonSerializer.Deserialize<Card>(jsonData);
        }
        //PUBLIC
        //
        //
        //
        public Deck() {
            CreateDeck();
            foreach(Card card in deck) {
                if (card.GetName().Length > 0) {
                    card.TagEffectsToCard();
                }
                else {
                    Console.WriteLine("Ill.");
                }
            }
        }
        public void TestDeck() {
            foreach(Card card in deck) {
                if (card.GetName().Length > 0) {
                    Console.WriteLine(card.GetName());
                    card.TagEffectsToCard();
                }
                else {
                    Console.WriteLine("Ill.");
                }
            }
        }
        public List<Card> DrawCards(int countToPull) {
            List<Card> movingCards = new();
            for (int i = 0; i < countToPull; i++) {
                try {
                    movingCards.Add(deck[i]);
                    movingCards.Remove(deck[i]);
                }
                catch {
                    movingCards.Add(dummyCard);
                    break;
                }
                i++;
            }
            return movingCards;
        }
    }
}
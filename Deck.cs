using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Game {
    public class Deck {
        //PRIVATE
        //
        //
        //
        
        private Card dummyCard;
        private List<Card> contents;
        private List<Card> CreateDeck() {
            string filePath = "./cardlist.json";
            // Read the JSON data from the file
            string json = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions{
                Converters = { new EffectConverter() },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var cards = JsonSerializer.Deserialize<List<Card>>(json, options);

            foreach (Card card in cards){ 
                Console.WriteLine("Card Name: " + card.GetName());
                foreach (var effect in card.Effects) {
                    Console.WriteLine("Effect: " + effect.GetName());
                }
            }

            //Create a "Burnout" card that acts as a dummy card in case
            //there are errors with the deck.
            filePath = "./dummycard.json";
            string dummyContents = File.ReadAllText(filePath);
            dummyCard = JsonSerializer.Deserialize<Card>(dummyContents);

            return cards;

            
        }
        //PUBLIC
        //
        //
        //
        public Deck() {
            contents = CreateDeck();
        }
        public void TestDeck() {
            foreach(Card card in contents) {
                if (card.GetName().Length > 0) {
                    Console.WriteLine(card.GetName());
                }
                else {
                    Console.WriteLine("Ill.");
                }
            }
        }
        public List<Card> DrawCards(int countToPull) {
            List<Card> movingCards = new();
            //A variable to store the 
            for (int i = 0; i < countToPull; i++) {
                Console.WriteLine("Adding card to hand.");
                try {
                    movingCards.Add(contents[i]);
                    contents.Remove(contents[i]);
                }
                catch {
                    movingCards.Add(dummyCard);
                    break;
                }
            }
            return movingCards;
        }
    }
}
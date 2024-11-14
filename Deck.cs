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
        private List<Servant> servants;
        private List<Card> CreateDeck() {
            string filePath = "./cardlist.json";
            // Read the JSON data from the file
            string json = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions{
                Converters = { new CardConverter() },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var cards = JsonSerializer.Deserialize<List<Card>>(json, options);

            foreach (Card card in cards){ 
                Console.WriteLine("Card Name: " + card.GetName());
                foreach (var effect in card.Effects) {
                    Console.WriteLine("Effect: " + effect.Name);
                }
            }

            //Create a "Burnout" card that acts as a dummy card in case
            //there are errors with the deck.
            filePath = "./dummycard.json";
            string dummyContents = File.ReadAllText(filePath);
            dummyCard = JsonSerializer.Deserialize<Card>(dummyContents);

            return cards;

            
        }
        private List<Servant> CreateServantDeck() {
            string filePath = "./servantlist.json";
            // Read the JSON data from the file
            string json = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions{
                Converters = { new CardConverter() },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var cards = JsonSerializer.Deserialize<List<Servant>>(json, options);

            foreach (Card card in cards){ 
                Console.WriteLine("Servant Name: " + card.GetName());
            }

            return cards;
        }
        //PUBLIC
        //
        //
        //
        public Deck() {
            contents = CreateDeck();
            servants = CreateServantDeck();
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
            //The cards moving to the hand
            List<Card> movingCards = new();
            //Iterate backwards because we are removing cards from the deck, shifting it each time, leading to a headache
            for (int i = countToPull - 1; i >= 0; i--) {
                Console.WriteLine("Adding card to hand.");
                try {
                    movingCards.Add(contents[i]);
                    contents.Remove(contents[i]);
                }
                catch {
                    movingCards.Add(dummyCard);
                }
            }
            return movingCards;
        }
        public List<Card> DrawServants(int countToPull) {
            List<Card> movingCards = new();
            for (int i = countToPull - 1; i >= 0; i--) {
                Console.WriteLine("Adding servant to servant hand");
                try {
                    movingCards.Add(servants[i]);
                    contents.Remove(servants[i]);
                }
                catch {
                    movingCards.Add(dummyCard);
                }
            }
            return movingCards;
        }
    }
}
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
        
        
        private void CreateDeck() {
            string filePath = "./cardlist.json";
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(filePath);

            // Deserialize the JSON string into a list of Card objects
            deck = JsonSerializer.Deserialize<List<Card>>(jsonData);
        }
        //PUBLIC
        //
        //
        //
        public Deck() {
            CreateDeck();
        }
        public void TestDeck() {
            foreach(Card card in deck) {
                if (card.GetName().Length > 0) {
                    Console.WriteLine(card.GetName());
                }
                else {
                    Console.WriteLine("Ill.");
                }
                card.TagEffectsToCard();
            }
        }
    }
}
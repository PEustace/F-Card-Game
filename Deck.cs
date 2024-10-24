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
        private void CreateDeck() {
            string filePath = "./cardlist.json";
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(filePath);

            // Deserialize the JSON string into a list of Card objects
            contents = JsonSerializer.Deserialize<List<Card>>(jsonData);

            //Create a "Burnout" card that acts as a dummy card in case
            //there are errors with the deck.
            filePath = "./dummycard.json";
            jsonData = File.ReadAllText(filePath);
            dummyCard = JsonSerializer.Deserialize<Card>(jsonData);
        }
        //Merges existing effect objects to cards based on the flags the card contains.
        private void MergeCardsEffectToCard() {
            foreach (Card card in contents) {
                //Try because a card may not have an effect string.
                try {
                    foreach (string effectString in card.EffectsStrings) {
                    Console.WriteLine("Found effect strings.");
                        switch (effectString) {
                            case "blockEnemySurvey": card.Effects.Add(new ChangeFlag("canSurvey", false)); break;
                            case "cardExpiresInBreak": card.Effects.Add(new ChangeFlag("expiresInBreak", true)); break;
                            case "revealEnemyCards": card.Effects.Add(new ChangeFlag("cardsRevealed", true)); break;
                        }
                    }
                }
                catch {
                    Console.WriteLine("Error: Card has no effects.");
                }
                
            }
        }
        //PUBLIC
        //
        //
        //
        public Deck() {
            CreateDeck();
            MergeCardsEffectToCard();
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
            for (int i = 0; i <= countToPull; i++) {
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
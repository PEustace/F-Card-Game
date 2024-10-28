using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Game {
    public interface IEffect {
        //PRIVATE
        //
        //
        //
        private static string name;
        //PUBLIC
        //
        //
        //
        public void Apply();
        public string GetName() {
            return name;
        }
    }
    //A converter to handle our card effect solution
    public class EffectConverter : JsonConverter<IEffect>
    {
        public override IEffect Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var rootElement = doc.RootElement;

                if (rootElement.TryGetProperty("ChangePlayerValue", out _))
                {
                    return JsonSerializer.Deserialize<ChangePlayerValue>(rootElement.GetRawText(), options);
                }

                if (rootElement.TryGetProperty("ChangeCardValue", out _))
                {
                    return JsonSerializer.Deserialize<ChangeCardValue>(rootElement.GetRawText(), options);
                }

                if (rootElement.TryGetProperty("ApplyDamage", out _))
                {
                    return JsonSerializer.Deserialize<ApplyDamage>(rootElement.GetRawText(), options);
                }

                Console.WriteLine("The card was not created due to unknown effect type.");
                throw new JsonException("Unknown effect type");
            }
        }

    public override void Write(Utf8JsonWriter writer, IEffect value, JsonSerializerOptions options)
    {
        // Implement if you need serialization back to JSON
        throw new NotImplementedException();
    }
}

    //Card games in OOP are tricky.
    //We need functions to handle different groups of things.
    //But we can't just make a class for every card (nor do I want to as the game data is stored externally in JSON)
    //Solution is to have effects run by checking a list of effect names on the cards.
    //Subject to change.
    public class ChangePlayerValue : IEffect {
        string name = "Change Player Value";
        public Dictionary<string, bool> newPlayerValue {get; set;} = new();
        public void Apply() {

        }
    }
    public class ChangeCardValue : IEffect {
        string name = "Change Card Value";
        public Dictionary<string, bool> newCardValue {get; set;} = new();
        public void Apply() {

        }
    }
    public class ApplyDamage : IEffect {
        string name = "Apply Damage";
        public string damageCount {get; set;}
        public string applyToType {get; set;}
        public void Apply() {

        }
    }
}
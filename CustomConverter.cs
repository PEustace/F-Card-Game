using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Game
{
    public class CardConverter : JsonConverter<Card>
    {
        public override Card Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                var root = jsonDoc.RootElement;
                var card = new Card
                {
                    Visible = root.GetProperty("Visible").GetBoolean(),
                    Name = root.GetProperty("Name").GetString(),
                    Type = root.GetProperty("Type").GetString(),
                    Faction = root.GetProperty("Faction").GetString(),
                    Body = root.GetProperty("Body").GetString(),
                    Health = root.GetProperty("Health").GetInt32(),
                    Cost = JsonSerializer.Deserialize<Dictionary<string, int>>(root.GetProperty("Cost").GetRawText(), options),
                    Effects = DeserializeEffects(root.GetProperty("Effects"), options)
                };
                return card;
            }
        }

        private List<IEffect> DeserializeEffects(JsonElement effectsElement, JsonSerializerOptions options)
        {
            var effects = new List<IEffect>();

            foreach (var effect in effectsElement.EnumerateArray())
            {
                if (effect.TryGetProperty("ChangePlayerValue", out var changePlayerValue))
                {
                    effects.Add(JsonSerializer.Deserialize<ChangePlayerValue>(changePlayerValue.GetRawText(), options));
                }
                else if (effect.TryGetProperty("ChangeCardValue", out var changeCardValue))
                {
                    effects.Add(JsonSerializer.Deserialize<ChangeCardValue>(changeCardValue.GetRawText(), options));
                }
                else if (effect.TryGetProperty("ApplyDamage", out var applyDamage))
                {
                    effects.Add(JsonSerializer.Deserialize<ApplyDamage>(applyDamage.GetRawText(), options));
                }
                else
                {
                    Console.WriteLine("Unknown effect type encountered.");
                    throw new JsonException("Unknown effect type encountered.");
                }
            }
            return effects;
        }

        public override void Write(Utf8JsonWriter writer, Card card, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteBoolean("Visible", card.Visible);
            writer.WriteString("Name", card.Name);
            writer.WriteString("Type", card.Type);
            writer.WriteString("Body", card.Body);

            // Serialize the Cost dictionary
            writer.WritePropertyName("Cost");
            JsonSerializer.Serialize(writer, card.Cost, options);

            // Serialize the Effects list
            writer.WritePropertyName("Effects");
            writer.WriteStartArray();
            foreach (var effect in card.Effects)
            {
                JsonSerializer.Serialize(writer, effect, effect.GetType(), options);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
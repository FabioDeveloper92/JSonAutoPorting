using System.Text.Json;

namespace JsonPorting.JsonPorting
{
    public class JsonPorting
    {
        private readonly bool _includeTargetDifferentKeys;
        public JsonPorting(bool includeTargetDifferentKeys)
        {
            _includeTargetDifferentKeys = includeTargetDifferentKeys;
        }


        public string CopyMissingKeysAndValues(string source, string targetJson)
        {
            using JsonDocument sourceDoc = JsonDocument.Parse(source);
            using JsonDocument targetDoc = JsonDocument.Parse(targetJson);

            return CopyMissingKeysAndValues(sourceDoc, targetDoc);
        }

        public string CopyMissingKeysAndValues(JsonDocument sourceDoc, JsonDocument targetDoc)
        {
            JsonElement sourceRoot = sourceDoc.RootElement;
            JsonElement targetRoot = targetDoc.RootElement;

            using var memoryStream = new MemoryStream();
            using (var writer = new Utf8JsonWriter(memoryStream, new JsonWriterOptions() { Indented = true }))
            {
                writer.WriteStartObject();

                CopyMissingProperties(sourceRoot, targetRoot, writer);

                writer.WriteEndObject();
            }

            return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        private void CopyMissingProperties(JsonElement source, JsonElement target, Utf8JsonWriter writer)
        {
            foreach (var property in source.EnumerateObject())
            {
                if (!target.TryGetProperty(property.Name, out JsonElement targetProperty))
                {
                    writer.WritePropertyName(property.Name);
                    property.Value.WriteTo(writer);
                }
                else
                {
                    if (property.Value.ValueKind == JsonValueKind.Object && targetProperty.ValueKind == JsonValueKind.Object)
                    {
                        writer.WritePropertyName(property.Name);
                        writer.WriteStartObject();
                        CopyMissingProperties(property.Value, targetProperty, writer);
                        writer.WriteEndObject();
                    }
                    else
                    {
                        writer.WritePropertyName(property.Name);
                        targetProperty.WriteTo(writer);
                    }
                }
            }

            if (_includeTargetDifferentKeys)
                foreach (var property in target.EnumerateObject())
                {
                    if (!source.TryGetProperty(property.Name, out _))
                    {
                        writer.WritePropertyName(property.Name);
                        property.Value.WriteTo(writer);
                    }
                }
        }
    }
}

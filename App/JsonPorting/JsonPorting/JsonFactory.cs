using System.Text.Json;

namespace JsonPorting.JsonPorting
{
    public static class JsonFactory
    {
        public static bool CompareTwoJson(string source, string target)
        {
            JsonDocument sourceDocument = JsonDocument.Parse(source);
            JsonDocument targetDocument = JsonDocument.Parse(target);

            var sourceJsonNormalize = RemoveWhitespace(sourceDocument.RootElement.GetRawText());
            var targetJsonNormalize = RemoveWhitespace(targetDocument.RootElement.GetRawText());

            return sourceJsonNormalize.Equals(targetJsonNormalize);
        }

        public static string RemoveWhitespace(string json)
        {
            return json
                .Replace(" ", "")
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace("\t", "");
        }
    }
}

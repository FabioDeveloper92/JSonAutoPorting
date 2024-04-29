using System.Text.Json;

namespace JsonPorting.JsonReader.Model
{
    public class JSonReaderInfo
    {
        public string FilePath { get; }
        public JsonDocument JsonDocument { get; }

        public JSonReaderInfo(string filePath, JsonDocument jsonDocument)
        {
            FilePath = filePath;
            JsonDocument = jsonDocument;
        }
    }
}

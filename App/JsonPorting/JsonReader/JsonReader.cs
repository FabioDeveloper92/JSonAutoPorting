using JsonPorting.JsonReader.Model;
using System.Text.Json;

namespace JsonPorting.JsonReader
{
    public class JsonReader
    {
        private readonly string _originalFileToCopy;
        private readonly string _fileSourceName;
        private readonly string _fileTargetName;

        public JsonReader(string originalFileToCopy, string fileSourceName, string fileTargetName)
        {
            _originalFileToCopy = originalFileToCopy;
            _fileSourceName = fileSourceName;
            _fileTargetName = fileTargetName;
        }

        public JsonDocument ReadFileSource()
        {
            string[] jsonFiles = Directory.GetFiles(_originalFileToCopy, _fileSourceName);

            foreach (var jsonFile in jsonFiles)
            {
                // Legge il contenuto del file JSON e lo trasforma in un documento JSON
                string jsonContent = File.ReadAllText(jsonFile);
                JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
                Console.WriteLine(_originalFileToCopy);
                return jsonDocument;
            }

            return null;
        }

        public JSonReaderInfo ReadFile(string itemConfigDirectory, string fileJsonToRead)
        {
            string[] jsonFiles = Directory.GetFiles(itemConfigDirectory, fileJsonToRead);

            foreach (var jsonFile in jsonFiles)
            {
                // Legge il contenuto del file JSON e lo trasforma in un documento JSON
                string jsonContent = File.ReadAllText(jsonFile);
                JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
                return new JSonReaderInfo(itemConfigDirectory + "\\" + fileJsonToRead, jsonDocument);
            }

            return null;
        }

        public IEnumerable<JSonReaderInfo> ReadJsonFilesInItemConfig(string rootDirectory, IEnumerable<string> subFolder)
        {
            // Esplora ricorsivamente tutte le sotto-cartelle
            foreach (var directory in Directory.GetDirectories(rootDirectory))
            {
                foreach (var jsonDocument in ReadJsonFilesInItemConfigRecursive(directory, subFolder))
                {
                    yield return jsonDocument;
                }
            }
        }

        private IEnumerable<JSonReaderInfo> ReadJsonFilesInItemConfigRecursive(string directory, IEnumerable<string> subFolder)
        {
            // Controlla se la cartella contiene 'item\config'
            var concatFolder = (new[] { directory }).Concat(subFolder).ToArray();

            string itemConfigDirectory = Path.Combine(concatFolder);
            if (Directory.Exists(itemConfigDirectory) && !itemConfigDirectory.Equals(_originalFileToCopy))
                yield return ReadFile(itemConfigDirectory, _fileTargetName);

            // Esplora ricorsivamente le sotto-cartelle
            foreach (var subdirectory in Directory.GetDirectories(directory))
                foreach (var jsonDocument in ReadJsonFilesInItemConfigRecursive(subdirectory, subFolder))
                    yield return jsonDocument;
        }
    }
}

using App.Model;
using JsonPorting.JsonReader;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("System", LogLevel.Warning)
        .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
        .AddConsole();
});
ILogger logger = loggerFactory.CreateLogger<Program>();


var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

var setting = configuration.Get<Settings>();

var rootDirectory = setting.RootDirectory;
var fileSourceName = setting.FileSource;
var sourceFileToCopy = setting.SourceFilePath;

var itemsSourceSplit = sourceFileToCopy.Split("\\");
var fileSourceAlign = itemsSourceSplit.Where(x => x.Contains(".json")).FirstOrDefault();
if (fileSourceAlign is not null)
{
    fileSourceName = fileSourceAlign;
    sourceFileToCopy = Path.GetDirectoryName(sourceFileToCopy);
}


var fileTargetName = setting.FileTarget;
var itemsTargetSplit = setting.SubDirectoryToSeachFileTarget.Split("\\");
var subFolder = itemsTargetSplit.Where(x => !x.Contains(".json")).ToArray();
var fileToAlign = itemsTargetSplit.Where(x => x.Contains(".json")).FirstOrDefault();
if (fileToAlign is not null)
    fileTargetName = fileToAlign;

var jsonReader = new JsonReader(sourceFileToCopy, fileSourceName, fileTargetName);

Console.WriteLine("Start");
Console.WriteLine("---------------------------");

foreach (var targetDocumentInfo in jsonReader.ReadJsonFilesInItemConfig(rootDirectory, subFolder))
{
    try
    {
        Console.WriteLine("");

        Console.WriteLine($"> {targetDocumentInfo.FilePath}");

        var jSonPorting = new JsonPorting.JsonPorting.JsonPorting(setting.IncludeTargetDifferentKeys);
        var res = jSonPorting.CopyMissingKeysAndValues(jsonReader.ReadFileSource(), targetDocumentInfo.JsonDocument);

        // Sovrascrivi il file con i nuovi dati JSON
        File.WriteAllText(targetDocumentInfo.FilePath, res);

        Console.WriteLine("");
    }
    catch (Exception ex)
    {
        logger.LogError("An error occurred: ", ex.Message);
    }
    finally
    {
        targetDocumentInfo.JsonDocument.Dispose();
    }

}

Console.WriteLine("---------------------------");
Console.WriteLine("Finish");
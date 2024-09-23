using System.Text.Json;
using RestCliClient.Core;
using RestCliClient.Core.Exceptions;
using RestCliClient.UI.Components;

namespace RestCliClient.UI;

public class ConsoleWindow: IDisplayWindow
{
    private bool _running = true;
    private readonly bool _debugMode;
    public Context Context { get; }
    public Logger Logger { get; }
    
    public ConsoleWindow(bool debugMode = false)
    {
        Context = new Context();
        Context.CommonNames = LoadCommonNames(AppDomain.CurrentDomain.BaseDirectory);
        _debugMode = debugMode;
        Logger = new Logger();
    }

    private Dictionary<string, string> LoadCommonNames(string appDir)
    {
        var commonNamesPath = Path.Join(appDir, "common-names.json");
        if(!File.Exists(commonNamesPath)) return new Dictionary<string, string>();
        
        var text = File.ReadAllText(commonNamesPath);
        return JsonSerializer.Deserialize<Dictionary<string, string>>(text) ?? throw new JsonException("Failed to load common names.json");
    }

    public void MainLoop()
    {
        while(_running)
        {
            try
            {
                var command = Context.GetPrompt().TakeCommand();
                command.Execute(Context, Logger);
            }
            catch (InvalidCommandException ex)
            {
                Console.WriteLine(ex.Message);
                if(_debugMode) Console.WriteLine(ex.ToString());
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                if(_debugMode) Console.WriteLine(ex.ToString());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                if(_debugMode) Console.WriteLine(ex.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unhandled error occured");
                if(_debugMode) Console.WriteLine(ex.ToString());
            }
        }
    }
    
    public void Close() => _running = false;
}
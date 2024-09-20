using RestCliClient.Core;
using RestCliClient.Core.Consts;

namespace RestCliClient.UI.Components;

public class BodyPrompt(Context ctx): IPrompt
{
    public ICommand TakeCommand()
    {
        Display();
        return CommandHandler.CreateCommand(Scopes.RequestBuilderBody, ctx, Console.ReadLine() ?? string.Empty);
    }
    
    private void Display()
    {
        Console.WriteLine("BODY> ");
    }
}
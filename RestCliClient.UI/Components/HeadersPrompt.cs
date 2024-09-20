using RestCliClient.Core;
using RestCliClient.Core.Consts;

namespace RestCliClient.UI.Components;

public class HeadersPrompt : IPrompt
{
    private readonly Context _context;
    public HeadersPrompt(Context context)
    {
        _context = context;
    }

    public ICommand TakeCommand()
    {
        Display();
        return CommandHandler.CreateCommand(Scopes.RequestBuilderHeaders, _context, Console.ReadLine() ?? string.Empty);
    }
    
    private void Display()
    {
        Console.WriteLine("HEADER> ");
    }
}
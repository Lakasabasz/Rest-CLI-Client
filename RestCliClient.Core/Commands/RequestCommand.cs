using RestCliClient.Core.Consts;

namespace RestCliClient.Core.Commands;

class RequestBuilderCommand: ICommand
{
    private string? _command;
    public IEnumerable<Scopes> ValidScopes => [Scopes.Global];
    
    public void Execute(Context context, ILogger logger)
    {
        //context.
    }

    public bool CanExecute(Context context, string command)
    {
        var toLower = command.ToLower();
        return toLower.StartsWith("get") 
            || toLower.StartsWith("post") 
            || toLower.StartsWith("put") 
            || toLower.StartsWith("patch")
            || toLower.StartsWith("delete");
    }

    public ICommand Command(string rawCommand)
    {
        _command = rawCommand;
        return this;
    }
}
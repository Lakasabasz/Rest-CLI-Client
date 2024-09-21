using RestCliClient.Core.Consts;
using RestCliClient.Core.Requests;

namespace RestCliClient.Core.Commands;

public class RequestBuilderCommand: ICommand
{
    private string? _command;
    public IEnumerable<Scopes> ValidScopes => [Scopes.Global];
    
    public void Execute(Context context, ILogger logger)
    {
        if(_command is null) throw new NullReferenceException("Command is null");
        var splitPoint = _command.IndexOf(' ');
        if(splitPoint < 0 || splitPoint == _command.Length - 1) throw new FormatException(Messages.INVALID_REQUEST_BUILDER_COMMAND);
        context.RequestBuilder = new RequestBuilder(_command[..splitPoint], _command[splitPoint..]);
        context.Scope = Scopes.RequestBuilderHeaders;
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
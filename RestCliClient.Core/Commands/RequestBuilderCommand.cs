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
        var splits = _command.Split(' ');
        if(splits.Length < 2 || splits[0].Length < 3 || splits[1].Length < 4) throw new FormatException(Messages.INVALID_REQUEST_BUILDER_COMMAND);
        context.RequestBuilder = new RequestBuilder(splits[0], splits[1].ResolveVariables(context));
        if(splits[2..].Contains("-k") || splits[2..].Contains("--ignore-ssl")) context.RequestBuilder.SetInsecureSsl(true);
        if(int.TryParse(splits[2..].FirstOrDefault(x => x.StartsWith("--timeout="))?.Split("=")[1], out int timeout))
            context.RequestBuilder.SetTimeout(timeout);
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
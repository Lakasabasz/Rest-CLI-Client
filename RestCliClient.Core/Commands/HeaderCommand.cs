using RestCliClient.Core.Consts;

namespace RestCliClient.Core.Commands;

public class HeaderCommand: ICommand
{
    private string? _rawCommand;
    public IEnumerable<Scopes> ValidScopes => [Scopes.RequestBuilderHeaders];
    public void Execute(Context context, ILogger logger)
    {
        if(context.RequestBuilder is null)
            throw new InvalidOperationException("RequestBuilder is null in scope RequestBuilderHeaders");
        if(string.IsNullOrWhiteSpace(_rawCommand))
        {
            context.Scope = Scopes.RequestBuilderBody;
            return;
        }
        var splitPoint = _rawCommand.IndexOf(':');
        if(splitPoint < 0) throw new FormatException(Messages.INVALID_HEADER_COMMAND);
        var key = _rawCommand[..splitPoint].Trim();
        var value = _rawCommand[splitPoint..].Trim();
        context.RequestBuilder.AddHeader(key, value);
    }

    public bool CanExecute(Context context, string command) => true;

    public ICommand Command(string rawCommand)
    {
        _rawCommand = rawCommand;
        return this;
    }
}
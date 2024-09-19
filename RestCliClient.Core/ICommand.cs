using RestCliClient.Core.Consts;

namespace RestCliClient.Core;

public interface ICommand
{
    IEnumerable<Scopes> ValidScopes { get; }
    public void Execute(Context context, ILogger logger);
    public bool CanExecute(Context context, string command);
    ICommand Command(string rawCommand);
}
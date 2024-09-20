using RestCliClient.Core.Consts;
using RestCliClient.Core.Exceptions;

namespace RestCliClient.Core;

public static class CommandHandler
{
    private static List<ICommand> Commands => typeof(ICommand).Assembly.GetTypes()
        .Where(t => typeof(ICommand).IsAssignableFrom(t))
        .Select(x => x.GetConstructor(Type.EmptyTypes)?.Invoke(null) as ICommand)
        .Where(x => x is not null)
        .ToList()!;
    
    public static ICommand CreateCommand(Scopes scope, Context context, string rawCommand)
    {
        return Commands.Where(x => x.ValidScopes.Contains(scope))
            .FirstOrDefault(x => x.CanExecute(context, rawCommand.Trim()))
            ?.Command(rawCommand.Trim()) ?? throw new InvalidCommandException(rawCommand);
    }
}
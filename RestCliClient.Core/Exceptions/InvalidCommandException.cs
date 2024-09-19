namespace RestCliClient.Core.Exceptions;

public class InvalidCommandException(string commandString): Exception(Consts.Messages.INVALID_COMMAND(commandString))
{
    
}
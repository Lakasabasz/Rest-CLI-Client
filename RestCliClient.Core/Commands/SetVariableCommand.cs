using RestCliClient.Core.Consts;

namespace RestCliClient.Core.Commands;

public class SetVariableCommand: ICommand
{
	private string? _rawCommand;
	public IEnumerable<Scopes> ValidScopes => [Scopes.Global];
	public void Execute(Context context, ILogger logger)
	{
		if(_rawCommand is null) throw new NullReferenceException("command is null");
		var split = _rawCommand.Split("=");
		if(split.Length != 2) throw new FormatException(Messages.INVALID_SET_VARIABLE_COMMAND);
		
		var variableName = split[0].TrimStart('$').Trim();
		if(string.IsNullOrWhiteSpace(variableName)) throw new FormatException(Messages.INVALID_SET_VARIABLE_COMMAND);
		var rawValue = split[1].Trim();
		string value;
		if(rawValue.StartsWith('$'))
		{
			if(context.LastRequest is null) throw new InvalidOperationException(Messages.MISSING_LAST_REQUEST);
			if(context.LastRequest.JsonContent is null) throw new NotSupportedException(Messages.LAST_REQUEST_NOT_JSON);
			value = context.LastRequest.JsonContent.GetValueByPath(rawValue);
		}
		else value = rawValue;

		context.Variables[variableName] = value;
	}
	public bool CanExecute(Context context, string command) => context.Scope == Scopes.Global && command.StartsWith('$');
	public ICommand Command(string rawCommand)
	{
		_rawCommand = rawCommand;
		return this;
	}
}
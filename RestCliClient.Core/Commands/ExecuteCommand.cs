using System.Text.Json;
using RestCliClient.Core.Consts;
using RestCliClient.Core.Sequence;

namespace RestCliClient.Core.Commands;

class ExecuteCommand: ICommand
{
	private string? _command;
	public IEnumerable<Scopes> ValidScopes => [Scopes.Global];
	public void Execute(Context context, ILogger logger)
	{
		
		if(_command is null) throw new NullReferenceException("Command is null");
		var fragments = _command.Split(" ");
		if(fragments.Length <= 1) throw new FormatException(Messages.INVALID_EXECUTE_COMMAND);
		var filename = fragments[1];
		var fileData = File.ReadAllText(filename + ".json");
		var sequenceModel = JsonSerializer.Deserialize<SequenceModel>(fileData);
		if(sequenceModel is null ) throw new FormatException(Messages.INVALID_SEQUENCE_FORMAT);
		SequenceHandler handler = new SequenceHandler(sequenceModel, context, logger);
		handler.Execute(fragments[2..].ToList());
	}
	
	public bool CanExecute(Context context, string command)
	{
		var toLower = command.ToLower();
		return toLower.StartsWith("execute");
	}
	
	public ICommand Command(string rawCommand)
	{
		_command = rawCommand;
		return this;
	}
}
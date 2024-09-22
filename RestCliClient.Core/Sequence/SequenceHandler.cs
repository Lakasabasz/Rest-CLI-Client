using RestCliClient.Core.Consts;
using RestCliClient.Core.Requests;

namespace RestCliClient.Core.Sequence;

public class SequenceHandler
{
	private readonly Context _globalContext;
	private readonly Context _internalContext;
	private readonly SequenceModel _sequenceModel;
	private readonly ILogger _logger;

	public SequenceHandler(SequenceModel sequence, Context globalContext, ILogger logger)
	{
		_internalContext = new Context();
		_sequenceModel = sequence;
		_globalContext = globalContext;
		_logger = logger;
	}
	
	public void Execute(List<string> args)
	{
		InitInternalContext(args);
		foreach (var (uri, method, headers, body, responseOperations) in _sequenceModel.sequence)
		{
			var builder = new RequestBuilder(method, uri.ResolveVariables(_internalContext));
			foreach (var (name, value) in headers) 
				builder.AddHeader(name.ResolveVariables(_internalContext), value.ResolveVariables(_internalContext));
			builder.AppendBody(body.ResolveVariables(_internalContext));
			_logger.LogMultiline($"Request: {method} {uri}", 
				$"{string.Join('\n', headers.Select(x => $"{x.name}: {x.value}"))}"
				+ "\n\n"
				+ body);
			var handler = new RequestHandler(builder, _logger);
			handler.Handle();
			_internalContext.LastRequest = handler.Response;
			foreach (var operation in responseOperations) PerformPostRequestOperation(operation);
		}
		foreach (var outVar in _sequenceModel.variables.@out) 
			_globalContext.Variables[outVar] = _internalContext.Variables[outVar];
	}
	
	private void InitInternalContext(List<string> args)
	{
		_internalContext.Variables = new Dictionary<string, string>(_globalContext.Variables);
		if(_sequenceModel.variables.@in.Length != args.Count) 
			throw new FormatException(Messages.INVALID_EXECUTE_ARGUMENTS_COUNT(_sequenceModel.variables.@in.Length, args.Count));
		foreach (var keyValuePair in _sequenceModel.variables.@in.Zip(args, (a, b) => new KeyValuePair<string, string>(a, b)))
		{
			_internalContext.Variables[keyValuePair.Key] = keyValuePair.Value;
		}
	}
	
	private void PerformPostRequestOperation(ResponseOperation operation)
	{
		VariableHelper.SetVariable(_internalContext, operation.variable, operation.value);
	}
}
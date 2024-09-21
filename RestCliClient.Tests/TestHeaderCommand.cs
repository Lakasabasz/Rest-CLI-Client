using RestCliClient.Core;
using RestCliClient.Core.Consts;
using RestCliClient.Core.Requests;
using RestCliClient.Tests.MockObjects;

namespace RestCliClient.Tests;

class TestHeaderCommand
{
	private Context _context;
	private ILogger _logger;
	
	[SetUp]
	public void Setup()
	{
		_context = new Context();
		_context.RequestBuilder = new RequestBuilder("get", "http://localhost:5000/");
		_context.Scope = Scopes.RequestBuilderHeaders;
		_logger = new TestLogger();
	}
	
	[Test]
	public void WithContent([Values("Content-Type: application/json", "Header:", "HeaderS: ")]string commands)
	{
		var cmd = CommandHandler
			.CreateCommand(Scopes.RequestBuilderHeaders, _context, commands);
		cmd.Execute(_context, _logger);
		Assert.That(_context.Scope, Is.EqualTo(Scopes.RequestBuilderHeaders));
	}
	
	[Test]
	public void Empty([Values("", " ")] string commands)
	{
		var cmd = CommandHandler.CreateCommand(Scopes.RequestBuilderHeaders, _context, commands);
		cmd.Execute(_context, _logger);
		Assert.That(_context.Scope, Is.EqualTo(Scopes.RequestBuilderBody));
	}

	private static List<string> _invalidCommands = [
		"test",
		":test",
		" :test"
	];
	
	[TestCaseSource(nameof(_invalidCommands))]
	public void WithInvalidCommands(string command)
	{
		var cmd = CommandHandler.CreateCommand(Scopes.RequestBuilderHeaders, _context, command);
		Assert.That(() => cmd.Execute(_context, _logger), 
			Throws.TypeOf<FormatException>().And.Message.EqualTo(Messages.INVALID_HEADER_COMMAND));
	}
}
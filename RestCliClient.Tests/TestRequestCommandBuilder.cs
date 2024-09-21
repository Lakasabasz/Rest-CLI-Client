using RestCliClient.Core;
using RestCliClient.Core.Consts;
using RestCliClient.Tests.MockObjects;

namespace RestCliClient.Tests;

class TestRequestCommandBuilder
{
	private Context _context;
	
	[SetUp]
	public void Setup()
	{
		_context = new Context();
		_context.Scope = Scopes.Global;
	}
	
	[Test]
	public void Simple()
	{
		var cmd = CommandHandler
			.CreateCommand(Scopes.Global, _context, "get https://example.com:8080/api/list?page=1&per_page=100");
		cmd.Execute(_context, new TestLogger());
		Assert.Multiple(() => 
		{
			Assert.That(_context.Scope, Is.EqualTo(Scopes.RequestBuilderHeaders));
			Assert.That(_context.RequestBuilder, Is.Not.Null);
		});
	}

	private static List<string> _invalidCommandsWithoutUrl = [
		"get",
		"get ",
		" get"
	];

	[TestCaseSource(nameof(_invalidCommandsWithoutUrl))]
	public void InvalidCommand(string rawCommand)
	{
		var cmd = CommandHandler
			.CreateCommand(Scopes.Global, _context, rawCommand);
		Assert.That(() => cmd.Execute(_context, new TestLogger()), 
			Throws.InstanceOf<FormatException>().With.Message.EqualTo(Messages.INVALID_REQUEST_BUILDER_COMMAND));
	}
}
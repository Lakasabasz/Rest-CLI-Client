using RestCliClient.Core;
using RestCliClient.Core.Consts;

namespace RestCliClient.Tests;

class TestBodyCommand
{
	private Context _context;
	private ILogger _logger;
	
	[SetUp]
	public void Setup()
	{
		_context = new Context();
		_context.Scope = Scopes.RequestBuilderBody;
		//_context.RequestBuilder = new RequestBuilder();
	}
}
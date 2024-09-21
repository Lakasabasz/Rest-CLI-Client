using RestCliClient.Core;
using RestCliClient.Core.Commands;
using RestCliClient.Core.Consts;
using RestCliClient.Core.Exceptions;
using RestCliClient.Core.Requests;

namespace RestCliClient.Tests;

class TestsCommandHandler
{
    private Context _ctx = null!;
    
    [SetUp]
    public void Setup()
    {
        _ctx = new Context();
    }
    
    private static IEnumerable<string> _validCommands = [
        "get", "Post", "paTCH", "DELETE", " put"
    ];
    
    [TestCaseSource(nameof(_validCommands))]
    public void ResolveRequestCommand(string command)
    {
        var cmd = CommandHandler.CreateCommand(Scopes.Global, _ctx, command);
        
        Assert.That(cmd.GetType(), Is.EqualTo(typeof(RequestBuilderCommand)));
    }

    private static IEnumerable<string> _invalidCommands = [
        "", " ", "dupa", "----", "DupA", " Dupa", "!", "#", "%", "^", "&", "(", ")"
    ];
    
    [TestCaseSource(nameof(_invalidCommands))]
    public void InvalidGlobalCommandResolve(string command)
    {
        Assert.That(() => CommandHandler.CreateCommand(Scopes.Global, _ctx, command),
            Throws.InstanceOf<InvalidCommandException>().And.Message.EqualTo(Messages.INVALID_COMMAND(command)));
    }
    
    [Test]
    public void ResolveHeaderCommand([Values("", "test: test", " ")] string command)
    {
        _ctx.RequestBuilder = new RequestBuilder("GET", "http://localhost:5000/");
        var cmd = CommandHandler.CreateCommand(Scopes.RequestBuilderHeaders, _ctx, command);
        
        Assert.That(cmd.GetType(), Is.EqualTo(typeof(HeaderCommand)));
    }
    
    [Test]
    public void ResolveBodyCommand([Values("", "test: test", " ")] string command)
    {
        _ctx.RequestBuilder = new RequestBuilder("GET", "http://localhost:5000/");
        var cmd = CommandHandler.CreateCommand(Scopes.RequestBuilderBody, _ctx, command);
        
        Assert.That(cmd.GetType(), Is.EqualTo(typeof(BodyCommand)));
    }
}
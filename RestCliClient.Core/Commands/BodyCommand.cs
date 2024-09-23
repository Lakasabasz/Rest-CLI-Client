using RestCliClient.Core.Consts;
using RestCliClient.Core.Requests;
using RestSharp;

namespace RestCliClient.Core.Commands;

public class BodyCommand: ICommand
{
    private string? _command;
    public IEnumerable<Scopes> ValidScopes => [Scopes.RequestBuilderBody];
    public void Execute(Context context, ILogger logger)
    {
        if(context.RequestBuilder is null)
            throw new InvalidOperationException("RequestBuilder is null in scope RequestBuilderHeaders");
        if(string.IsNullOrEmpty(_command))
        {
            var handler = new RequestHandler(context.RequestBuilder, logger);
            try
            {
                handler.Handle();
            }
            catch(Exception ex)
            {
                context.Scope = Scopes.Global;
                throw;
            }
            context.LastRequest = handler.Response;
            context.Scope = Scopes.Global;
            return;
        }
        context.RequestBuilder.AppendBody(_command.ResolveVariables(context));
    }

    public bool CanExecute(Context context, string command) => true;

    public ICommand Command(string rawCommand)
    {
        _command = rawCommand;
        return this;
    }
}
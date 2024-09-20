using RestCliClient.Core;
using RestCliClient.Core.Consts;
using RestCliClient.UI.Components;

namespace RestCliClient.UI;

static class ContextExtensions
{
    public static IPrompt GetPrompt(this Context context)
    {
        return context.Scope switch
        {
            Scopes.Global => new GlobalPrompt(context),
            Scopes.RequestBuilderHeaders => new HeadersPrompt(context),
            Scopes.RequestBuilderBody => new BodyPrompt(context),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}
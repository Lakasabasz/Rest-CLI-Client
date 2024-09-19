using RestCliClient.Core;
using RestCliClient.UI.Components;

namespace RestCliClient.UI;

static class ContextExtensions
{
    public static GlobalPrompt GetPrompt(this Context context)
    {
        return new GlobalPrompt(context);
    }
}
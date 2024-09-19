using RestCliClient.Core.Consts;

namespace RestCliClient.Core;

public class Context
{
    public Scopes Scope { get; set; } = Scopes.Global;
    public string? LastRequest { get; set; } = null;
}
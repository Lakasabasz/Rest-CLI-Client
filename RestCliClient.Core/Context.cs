using RestCliClient.Core.Consts;
using RestCliClient.Core.Requests;

namespace RestCliClient.Core;

public class Context
{
    public Scopes Scope { get; set; } = Scopes.Global;
    public string? LastRequest { get; set; } = null;
    public RequestBuilder? RequestBuilder { get; set; }
}
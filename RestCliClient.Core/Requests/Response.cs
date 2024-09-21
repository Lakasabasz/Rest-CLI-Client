using RestSharp;

namespace RestCliClient.Core.Requests;

public class Response
{    
    public Response(RestResponse response)
    {
        Method = response.Request.Method;
        Uri = $"{response.ResponseUri?.Scheme}://{response.ResponseUri?.Host}:{response.ResponseUri?.Port}{response.ResponseUri?.LocalPath}";
        ResponseContent = response.Content ?? string.Empty;
        JsonContent = JsonObject.TryParse(ResponseContent, out var json) ? json : null;
    }

    public Method Method { get; }
    public string Uri { get; }
    
    public string ResponseContent { get; }
    
    public JsonObject? JsonContent { get; }
}
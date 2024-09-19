using System.Text.Json.Nodes;
using RestSharp;

namespace RestCliClient.Core.Requests;

public class RequestBuilder
{
    private readonly Method _method;
    private readonly Uri _uri;
    private readonly List<KeyValuePair<string, string>> _headers;
    private JsonObject _body;
    
    public RequestBuilder(string method, string url)
    {
        if(!Enum.TryParse(method, true, out _method)) throw new ArgumentException("Method is not valid");
        _uri = new Uri(url);
        _headers = [];
    }
    
    public RequestBuilder AddHeader(string name, string value)
    {
        _headers.Add(new KeyValuePair<string, string>(name, value));
        return this;
    }
    
    public RequestBuilder AddBody(string json)
    {
        _body = new JsonObject(json);
        return this;
    }
    
    public RestRequest BuildRequest()
    {
        var request = new RestRequest(_uri, _method)
            .AddHeaders(_headers)
            .AddBody(_body.ToString());
        return request;
    }

    public RestClient BuildClient() => new RestClient(_uri);
}
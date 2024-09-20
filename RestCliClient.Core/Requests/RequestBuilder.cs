using System.Text.Json.Nodes;
using RestSharp;

namespace RestCliClient.Core.Requests;

public class RequestBuilder
{
    private readonly Method _method;
    private readonly Uri _uri;
    private readonly List<KeyValuePair<string, string>> _headers;
    private string _body;
    
    public RequestBuilder(string method, string url)
    {
        if(!Enum.TryParse(method, true, out _method)) throw new ArgumentException("Method is not valid");
        _uri = new Uri(url);
        _headers = [];
        _body = string.Empty;
    }
    
    public RequestBuilder AddHeader(string name, string value)
    {
        _headers.Add(new KeyValuePair<string, string>(name, value));
        return this;
    }
    
    public RequestBuilder AppendBody(string bodyPart)
    {
        _body += bodyPart;
        return this;
    }
    
    public RestRequest BuildRequest()
    {
        var request = new RestRequest(_uri, _method)
            .AddHeaders(_headers)
            .AddBody(_body);
        var queryParams = _uri.Query.Split('&');
        foreach (string queryParam in queryParams)
        {
            var parts = queryParam.Split('=');
            request.AddQueryParameter(parts[0], parts[1]);
        }
        return request;
    }

    public RestClient BuildClient() => new RestClient(_uri);
}
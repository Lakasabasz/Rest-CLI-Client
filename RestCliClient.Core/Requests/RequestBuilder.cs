using System.Net.Security;
using System.Text.Json.Nodes;
using RestSharp;

namespace RestCliClient.Core.Requests;

public class RequestBuilder
{
    private readonly Method _method;
    private readonly Uri _uri;
    private readonly List<KeyValuePair<string, string>> _headers;
    private string _body;
    private bool _insecureSsl = false;
    private int _timeout = 60;
    
    public RequestBuilder(string method, string url)
    {
        if(!Enum.TryParse(method, true, out _method)) throw new ArgumentException("Method is not valid");
        _uri = new Uri(url);
        _headers = [];
        _body = string.Empty;
    }
    
    public RequestBuilder SetInsecureSsl(bool insecureSsl)
    {
        _insecureSsl = insecureSsl;
        return this;
    }
    
    public RequestBuilder SetTimeout(int timeout)
    {
        _timeout = timeout;
        return this;
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
        var queryParams = _uri.Query.Split('&').Where(x => !string.IsNullOrWhiteSpace(x));
        foreach (string queryParam in queryParams)
        {
            var parts = queryParam.Split('=');
            request.AddQueryParameter(parts[0], parts[1]);
        }
        return request;
    }

    public RestClient BuildClient()
    {
        return new RestClient(_uri, opt => 
        {
            opt.RemoteCertificateValidationCallback = _insecureSsl ? ((_, _, _, _) => true) : null;
            opt.Timeout = new TimeSpan(0, 0, _timeout);
        });
    }
}
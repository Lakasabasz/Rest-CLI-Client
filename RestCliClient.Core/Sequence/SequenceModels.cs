// ReSharper disable InconsistentNaming

using System.Reflection;
using System.Text.Json.Serialization;

namespace RestCliClient.Core.Sequence;

public class SequenceModel(
    Variables? variables,
    Sequence[] sequence
)
{
    public Variables? variables { get; init; } = variables;
    [JsonRequired] public Sequence[] sequence { get; init; } = sequence;
}

public record Variables(
    string[]? @in,
    string[]? @out
);


public class Sequence(
    string uri,
    string method,
    Headers[]? headers,
    object? body,
    ResponseOperation[] response_operations,
    Options? options
)
{
    [JsonRequired] public string uri { get; init; } = uri;
    [JsonRequired] public string method { get; init; } = method;
    public Headers[]? headers { get; init; } = headers;
    public object? body { get; init; } = body;
    public ResponseOperation[]? response_operations { get; init; } = response_operations;
    public Options? options { get; init; } = options;

    public void Deconstruct(out string uri, out string method, out Headers[]? headers, out object? body, out ResponseOperation[]? response_operations, out Options? options)
    {
        uri = this.uri;
        method = this.method;
        headers = this.headers;
        body = this.body;
        response_operations = this.response_operations;
        options = this.options;
    }
}

public record Options(
    bool? ignore_ssl,
    int? timeout
);

public class Headers(
    string name,
    string? value
)
{
    [JsonRequired] public string name { get; init; } = name;
    public string? value { get; init; } = value;

    public void Deconstruct(out string name, out string? value)
    {
        name = this.name;
        value = this.value;
    }
}

public class ResponseOperation(
    string variable,
    string value
)
{
    [JsonRequired] public string variable { get; init; } = variable;
    [JsonRequired] public string value { get; init; } = value;
}


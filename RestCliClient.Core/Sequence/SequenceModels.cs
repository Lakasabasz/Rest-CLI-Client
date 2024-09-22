// ReSharper disable InconsistentNaming
namespace RestCliClient.Core.Sequence;

public record SequenceModel(
    string name,
    Variables variables,
    Sequence[] sequence
);

public record Variables(
    string[] @in,
    string[] @internal,
    string[] @out
);

public record Sequence(
    string uri,
    string method,
    Headers[] headers,
    string body,
    ResponseOperation[] response_operations
);

public record Headers(
    string name,
    string value
);

public record ResponseOperation(
    string variable,
    string value
);


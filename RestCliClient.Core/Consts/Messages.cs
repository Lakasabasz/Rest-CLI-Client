using System.Net;

namespace RestCliClient.Core.Consts;

public static class Messages
{
    public static string INVALID_COMMAND(string command) => $"Command not found: {command}";

    public const string INVALID_REQUEST_BUILDER_COMMAND =
        """
        Request command uses format: `<GET|POST|PUT|DELETE|PATCH> <url>`
        - url: must have specified protocol (http(s)://) and it can contain query parameters
        
        Example: GET https://example.com:8080/api?version=1.0
        """;

    public const string INVALID_HEADER_COMMAND =
        """
        To add new header use format: `<key>:<value>`
        For example: `Content-Type: application/json`
        
        If you want to go next press enter without content
        """;

    public const string REQUEST_IN_PROGRESS = "Request in progress...";
    public const string CONNECTION_ERROR = "Error appered during connection to server";
    public const string RESPONSE_HEADER = "Response headers";
    public const string RESPONSE_BODY = "Response body";

    public static string REQUEST_RESULT_WITH_CODE(HttpStatusCode response)
        => $"Request result with code {(int)response}";
}
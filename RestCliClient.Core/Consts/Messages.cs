using System.Net;

namespace RestCliClient.Core.Consts;

public static class Messages
{
    public static string INVALID_COMMAND(string command) => $"Command not found: {command}";

    public const string INVALID_REQUEST_BUILDER_COMMAND =
        """
        Request command uses format: `<GET|POST|PUT|DELETE|PATCH> <url>`
        - url: have to have specified protocol (http(s)://) and it can contain query parameters
        
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
    public const string INVALID_SET_VARIABLE_COMMAND = 
        """
        To set variable use format: `$<variable_name>=[value]`
        - variable_name: musn't whitespaces
        - value: it can contain raw value or json path
        
        For example:
        - `$token=Bearer jwt.jwt.jwt`
        - `$token=$.users[1].token`
        """;

    public const string MISSING_LAST_REQUEST = "Cannot set variable using json path without finished request before";
    public const string LAST_REQUEST_NOT_JSON = "Setting variable using json path with non-json response from request before is not supported";

    public static string REQUEST_RESULT_WITH_CODE(HttpStatusCode response)
        => $"Request result with code {(int)response}";
}
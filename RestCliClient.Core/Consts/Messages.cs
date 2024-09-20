namespace RestCliClient.Core.Consts;

static class Messages
{
    public static string INVALID_COMMAND(string command) => $"Command not found: {command}";

    public static string INVALID_REQUEST_BUILDER_COMMAND =>
        """
        Request command uses format: `<GET|POST|PUT|DELETE|PATCH> <url>`
        - url: must have specified protocol (http(s)://) and it can contain query parameters
        
        Example: GET https://example.com:8080/api?version=1.0
        """;
}
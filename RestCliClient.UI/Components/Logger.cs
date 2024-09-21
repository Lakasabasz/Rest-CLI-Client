using RestCliClient.Core;

namespace RestCliClient.UI.Components;

public class Logger: ILogger
{
    public void LogLine(string message)
    {
        Console.WriteLine($"> {message}");
    }

    public void LogMultiline(string title, string message, bool closed = true)
    {
        Console.WriteLine($">>> {title}");
        Console.WriteLine(message);
        if(closed) Console.WriteLine(">>>");
    }

    public void LogError(string message)
    {
        throw new NotImplementedException();
    }
}
namespace RestCliClient.Core;

public interface ILogger
{
    public void LogLine(string message);
    
    public void LogMultiline(string title, string message, bool closed = true);
    
    public void LogError(string message); 
}
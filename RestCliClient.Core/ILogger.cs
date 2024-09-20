namespace RestCliClient.Core;

public interface ILogger
{
    public void LogLine(string message);
    
    public void LogMultiline(string title, string message);
    
    public void LogError(string message); 
}
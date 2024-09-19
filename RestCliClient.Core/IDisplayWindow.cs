namespace RestCliClient.Core;

public interface IDisplayWindow
{
    Context Context { get; }
    void Close();
}
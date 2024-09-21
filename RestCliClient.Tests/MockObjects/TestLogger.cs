using RestCliClient.Core;

namespace RestCliClient.Tests.MockObjects;

class TestLogger: ILogger
{
	public List<(string?, string)> Messages { get; } = [];

	public void LogLine(string message) => Messages.Add((null, message));
	public void LogMultiline(string title, string message) => Messages.Add((title, message));
	public void LogError(string message) => Messages.Add((null, message));
}
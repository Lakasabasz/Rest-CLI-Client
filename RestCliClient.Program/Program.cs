using System.Reflection;
using RestCliClient.UI;

string helloPrompt = 
$"""
Rest Cli Client (version: {Assembly.GetExecutingAssembly().GetName().Version})
""";

Console.WriteLine(helloPrompt);

var consoleWindow = new ConsoleWindow(args.Contains("--debug"));
consoleWindow.MainLoop();

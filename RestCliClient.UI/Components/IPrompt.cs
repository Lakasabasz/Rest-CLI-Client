using RestCliClient.Core;

namespace RestCliClient.UI.Components;

public interface IPrompt
{
    ICommand TakeCommand();
}
using RestCliClient.Core.Consts;

namespace RestCliClient.Core;

public static class VariableHelper
{
	public static void SetVariable(Context context, string name, string rawValue)
	{
		string value;
		if(rawValue.StartsWith('$'))
		{
			if(context.LastRequest is null) throw new InvalidOperationException(Messages.MISSING_LAST_REQUEST);
			if(context.LastRequest.JsonContent is null) throw new NotSupportedException(Messages.LAST_REQUEST_NOT_JSON);
			value = context.LastRequest.JsonContent.GetValueByPath(rawValue);
		}
		else value = rawValue;

		context.Variables[name] = value;
	}
}
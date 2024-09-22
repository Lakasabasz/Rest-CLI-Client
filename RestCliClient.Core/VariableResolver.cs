namespace RestCliClient.Core;

public static class VariableResolver
{
	public static string ResolveVariables(this string str, Context context)
	{
		List<int> triggerPositions = [];
		var currentPosition = 0;
		while (currentPosition < str.Length)
		{
			var position = str.IndexOf('$', currentPosition);
			if(position == -1) break;
			triggerPositions.Add(position);
			currentPosition = position + 1;
		}

		List<string> parts = [];
		var lastPos = 0;
		foreach (var i in triggerPositions)
		{
			parts.Add(str[lastPos..i]);
			lastPos = i;
		}
		parts.Add(str[lastPos..]);
		
		string combined = string.Empty;
		var skipNext = false;
		foreach (var part in parts)
		{
			if(skipNext)
			{
				combined += part;
				skipNext = false;
				continue;
			}
			if(part.Length < 1) continue;
			if(part == "$")
			{
				skipNext = true;
				continue;
			}

			var key = context.Variables.Keys
				.Where(variablesKey => part.StartsWith("$" + variablesKey))
				.MaxBy(x => x.Length);
			if(key is null)
			{
				combined += part;
				continue;
			}
			combined += part.Replace("$" + key, context.Variables[key]);
		}
		
		return combined;
	}
}
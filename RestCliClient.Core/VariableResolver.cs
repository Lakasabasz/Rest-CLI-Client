namespace RestCliClient.Core;

public static class VariableResolver
{
	private static List<string> Split(string str, char separator)
	{
		List<string> parts = [];
		var lastPos = 0;
		var currentPosition = 0;
		while (currentPosition < str.Length)
		{
			var position = str.IndexOf(separator, currentPosition);
			if(position == -1)
			{
				parts.Add(str[lastPos..]);
				break;
			}
			currentPosition = position + 1;
			parts.Add(str[lastPos..position]);
			lastPos = position;
		}
		return parts;
	}
	
	private static string Resolve(string str, Dictionary<string, string> variables, char trigger)
	{
		var parts = Split(str, trigger);

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
			if(part == $"{trigger}")
			{
				skipNext = true;
				continue;
			}

			var key = variables.Keys
				.Where(variablesKey => part.StartsWith($"{trigger}" + variablesKey))
				.MaxBy(x => x.Length);
			if(key is null)
			{
				combined += part;
				continue;
			}
			combined += part.Replace($"{trigger}" + key, variables[key]);
		} 
		return combined;
	}
	
	public static string ResolveVariables(this string str, Context context) => Resolve(str, context.Variables, '$');

	public static string ResolveCommonName(this string str, Context context) => Resolve(str, context.CommonNames, '@');
}
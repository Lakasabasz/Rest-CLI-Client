using RestCliClient.Core;

namespace RestCliClient.Tests;

class TestVariableResolve
{
	private Context _context;
	
	[OneTimeSetUp]
	public void Setup()
	{
		_context = new Context();
		_context.Variables = [];
		_context.Variables["var"] = "2137";
		_context.Variables["variable"] = "papaj";
	}

	private static List<(string, string)> _testCases = [
		("$var", "2137"),
		("$variable", "papaj"),
		("$dupa", "$dupa"),
		("https://example.com/$var", "https://example.com/2137"),
		("https://example.com/$$var", "https://example.com/$var"),
		("https://example.com/$variable/image", "https://example.com/papaj/image"),
		("{\"name\": \"Bearer $var\"}", "{\"name\": \"Bearer 2137\"}"),
		("$var$variable$var", "2137papaj2137"),
		("abc$vardef$variableghi$varjkl", "abc2137defpapajghi2137jkl")
	];
	
	[TestCaseSource(nameof(_testCases))]
	public void ResolveVariable((string value, string expected) data)
	{
		Assert.That(data.value.ResolveVariables(_context), Is.EqualTo(data.expected));
	}
}
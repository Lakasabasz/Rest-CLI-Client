using System.Text.Json.Nodes;
using RestCliClient.Core.Exceptions;

namespace RestCliClient.Core;

public class JsonObject
{
    private readonly JsonNode _json;
    public JsonObject(string rawJson)
    {
        _json = JsonNode.Parse(rawJson) ?? throw new InvalidJsonException();
    }
    
    public static bool TryParse(string rawJson, out JsonObject? jsonObject)
    {
        try
        {
            jsonObject = new JsonObject(rawJson);
            return true;
        }
        catch (InvalidJsonException)
        {
            jsonObject = null;
            return false;
        }
    }

    public override string ToString() => _json.ToJsonString();
}
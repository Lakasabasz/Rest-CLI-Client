using System.Text.Json;
using System.Text.Json.Nodes;
using Json.Path;
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
        catch(JsonException)
        {
            jsonObject = null;
            return false;
        }
    }

    public override string ToString() => _json.ToJsonString();
    public string GetValueByPath(string rawValue)
    {
        var path = JsonPath.Parse(rawValue);
        var result = path.Evaluate(_json);
        var match = result.Matches.FirstOrDefault();
        
        if(match is null) throw new InvalidOperationException(Consts.Messages.JSON_PATH_NOT_FOUND(rawValue));
        return match.Value?.ToString() ?? throw new InvalidOperationException(Consts.Messages.JSON_PATH_NOT_FOUND(rawValue));
    }
}
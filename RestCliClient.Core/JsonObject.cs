using System.Text.Json.Nodes;
using RestCliClient.Core.Exceptions;

namespace RestCliClient.Core;

public class JsonObject
{
    private JsonNode _json;
    public JsonObject(string rawJson)
    {
        _json = JsonNode.Parse(rawJson) ?? throw new InvalidJsonException();
    }
}
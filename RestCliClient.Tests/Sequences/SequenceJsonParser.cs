using System.Text.Json;
using RestCliClient.Core.Sequence;

namespace RestCliClient.Tests.Sequences;

public class SequenceJsonParser
{
    [Test]
    public void ReadMeExampleJson()
    {
        var parsedModel = JsonSerializer.Deserialize<SequenceModel>(TestConsts.ReadMeJson);
        Assert.That(parsedModel, Is.Not.Null);
        Assert.Multiple(() => 
        {
            Assert.That(parsedModel.variables?.@in, Is.EquivalentTo((List<string>)["token", "username", "password"]));
            Assert.That(parsedModel.variables?.@out, Is.EquivalentTo((List<string>)["user-id", "user-token"]));
            Assert.That(parsedModel.sequence.First().body, Is.EqualTo("{\"username\": \"$username\", \"password\": \"$password\", \"category\": \"$$\"}"));
            Assert.That(parsedModel.sequence.First().headers?.Select(x => (x.name, x.value)), 
                Is.EquivalentTo((IEnumerable<(string, string)>)[("Authorization", "Bearer $token"), ("Content-Type", "application/json")]));
            Assert.That(parsedModel.sequence.First().options?.timeout, Is.EqualTo(15));
            Assert.That(parsedModel.sequence.First().options?.ignore_ssl, Is.EqualTo(false));
            Assert.That(parsedModel.sequence.First().response_operations?.Select(x => (x.variable, x.value)), 
                Is.EquivalentTo((List<(string, string)>)[("user-guid", "$.user.id")]));
            Assert.That(parsedModel.sequence.First().uri, Is.EqualTo("https://example.com/api/users"));
            Assert.That(parsedModel.sequence.First().method, Is.EqualTo("POST"));
        });
    }
    
    [Test]
    public void ReadMeExampleMinimalJson()
    {
        var parsedModel = JsonSerializer.Deserialize<SequenceModel>(TestConsts.ReadMeMinimalJson);
        Assert.That(parsedModel, Is.Not.Null);
        Assert.Multiple(() => 
        {
            Assert.That(parsedModel.sequence.First().uri, Is.EqualTo("https://example.com/api/users"));
            Assert.That(parsedModel.sequence.First().method, Is.EqualTo("POST"));
        });
    }
}
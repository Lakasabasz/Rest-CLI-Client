using RestSharp;

namespace RestCliClient.Core.Requests;

public class RequestHandler(RequestBuilder builder, ILogger logger)
{
    public void Handle()
    {
        using var client = builder.BuildClient();
        var request = builder.BuildRequest();

        logger.LogLine(Consts.Messages.REQUEST_IN_PROGRESS);
        var response = client.Execute(request);
        if(response.StatusCode == 0)
        {
            logger.LogError(Consts.Messages.CONNECTION_ERROR);
            return;
        }
        logger.LogLine(Consts.Messages.REQUEST_RESULT_WITH_CODE(response.StatusCode));
        logger.LogMultiline(Consts.Messages.RESPONSE_HEADER,
            string.Join("\n", response.Headers?.Select(x => $"{x.Name}={x.Value}") ?? []));
        logger.LogMultiline(Consts.Messages.RESPONSE_BODY, response.Content ?? string.Empty);
        Response = new Response(response);
    }

    public Response? Response { get; private set; }
}
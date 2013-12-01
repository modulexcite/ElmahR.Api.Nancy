using ElmahR.Api.Messages;

namespace ElmahR.Api.Client {
    public interface IElmahRApiClient {
        ErrorSubmitResponse Send(ErrorListSubmitRequest errorListSubmitRequest);
    }
}
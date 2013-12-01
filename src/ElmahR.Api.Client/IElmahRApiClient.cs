using ElmahR.Api.Messages;

namespace ElmahR.Api.Client {
    public interface IElmahRApiClient {
        ErrorSubmitResponse Post(ErrorListSubmitRequest errorListSubmitRequest);
        ErrorSubmitResponse Post(ErrorSubmitRequest errorSubmitRequest);
    }
}
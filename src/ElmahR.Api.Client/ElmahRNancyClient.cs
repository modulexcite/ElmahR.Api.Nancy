using ElmahR.Api.Messages;
using RestSharp;

namespace ElmahR.Api.Client {
    public class ElmahRNancyClient : IElmahRApiClient {
        const string NANCYAPI_SUBMITERRORS = "nancyapi/submitErrors";
        const string NANCYAPI_SUBMITERROR = "nancyapi/submitError";

        private readonly string _baseUrl;

        public ElmahRNancyClient(string baseUrl) {
            _baseUrl = baseUrl;
        }

        public ErrorSubmitResponse Send(ErrorListSubmitRequest errorListSubmitRequest) {
            RestRequest request;
            var client = PrepareRestClientWithRequestBody(errorListSubmitRequest, NANCYAPI_SUBMITERRORS, out request);
            IRestResponse<ErrorSubmitResponse> response = client.Post<ErrorSubmitResponse>(request);
            return response.Data;
        }
        
        public ErrorSubmitResponse Send(ErrorSubmitRequest errorSubmitRequest) {
            RestRequest request;
            var client = PrepareRestClientWithRequestBody(errorSubmitRequest, NANCYAPI_SUBMITERROR, out request);
            IRestResponse<ErrorSubmitResponse> response = client.Post<ErrorSubmitResponse>(request);
            return response.Data;
        }

        private RestClient PrepareRestClientWithRequestBody(object requestBody,
            string nancyapiSubmiterrors, out RestRequest request) {
            var client = new RestClient(string.Format("{0}{1}", _baseUrl, nancyapiSubmiterrors));
            request = new RestRequest { RequestFormat = DataFormat.Json };
            request.AddBody(requestBody);
            return client;
        }
    }
}

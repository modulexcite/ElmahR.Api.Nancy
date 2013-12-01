using System;
using ElmahR.Api.Client;
using ElmahR.Api.Messages;

namespace ElmahR.ConsoleSource {
    class Program {
        static void Main() {

            ErrorListSubmitRequest errorListSubmitRequest = SampleRequestFixture.CreateErrorListSubmitRequest();

            IElmahRApiClient apiClient = new ElmahRNancyClient("http://dashboard.elmahrnancyapi.com/");
            ErrorSubmitResponse response = apiClient.Post(errorListSubmitRequest);

            Console.WriteLine("Status : {0}, Message : {1}", response.Status, response.StatusMessage);

            Console.ReadKey();
        }
    }
}

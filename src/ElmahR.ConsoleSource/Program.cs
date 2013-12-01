using System;
using ElmahR.Api.Client;
using ElmahR.Api.Messages;

namespace ElmahR.ConsoleSource {
    class Program {
        static void Main() {

            ErrorListSubmitRequest errorListSubmitRequest = SampleRequestFixture.CreateErrorListSubmitRequest();

            IElmahRApiClient apiClient = new ElmahRNancyClient("http://dashboard.elmahrnancyapi.com/");
            ErrorSubmitResponse response = apiClient.Post(errorListSubmitRequest);

            Console.WriteLine("Status : {0}, Desc : {1}", response.SubmitionStatus, response.SubmitionStatusDescription);

            Console.ReadKey();
        }
    }
}

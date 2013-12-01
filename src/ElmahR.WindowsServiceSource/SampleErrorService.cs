using System;
using ElmahR.Api.Client;
using ElmahR.Api.Messages;
using ElmahR.ConsoleSource;

namespace ElmahR.WindowsServiceSource {
    public class SampleErrorService {
        public SampleErrorService() {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            /*TODO Convert errors to ErrorDto*/
        }
        public void Start() {
            ErrorListSubmitRequest errorListSubmitRequest = SampleRequestFixture.CreateErrorListSubmitRequest("Win Service Source","Start Action");

            IElmahRApiClient apiClient = new ElmahRNancyClient("http://dashboard.elmahrnancyapi.com/");
            ErrorSubmitResponse response = apiClient.Post(errorListSubmitRequest);

            Console.WriteLine("Status : {0}, Desc : {1}", response.SubmitionStatus, response.SubmitionStatusDescription);
        }

        public void Stop() {
            ErrorListSubmitRequest errorListSubmitRequest = SampleRequestFixture.CreateErrorListSubmitRequest("Win Service Source", "Stop Action");

            IElmahRApiClient apiClient = new ElmahRNancyClient("http://dashboard.elmahrnancyapi.com/");
            ErrorSubmitResponse response = apiClient.Post(errorListSubmitRequest);

            Console.WriteLine("Status : {0}, Desc : {1}", response.SubmitionStatus, response.SubmitionStatusDescription);
        }
    }
}
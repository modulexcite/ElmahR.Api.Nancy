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
            PostSampleError("Windows Service Started");
        }

        public void Stop() {
            PostSampleError("Windows Service Stopped");
        }

        private static void PostSampleError(string detail) {
            ErrorListSubmitRequest errorListSubmitRequest = SampleRequestFixture.CreateErrorListSubmitRequest("Win Service Source", detail);

            IElmahRApiClient apiClient = new ElmahRNancyClient("http://dashboard.elmahrnancyapi.com/");
            apiClient.Post(errorListSubmitRequest);
        }
    }
}
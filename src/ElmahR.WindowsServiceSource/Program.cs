using Topshelf;

namespace ElmahR.WindowsServiceSource {
    class Program {
        static void Main(string[] args) {
            HostFactory.Run(hostConfigurator => {
                hostConfigurator.Service<SampleErrorService>(serviceConfigurator => {
                    serviceConfigurator.ConstructUsing(name => new SampleErrorService());
                    serviceConfigurator.WhenStarted(tc => tc.Start());
                    serviceConfigurator.WhenStopped(tc => tc.Stop());
                });

                hostConfigurator.RunAsNetworkService();
                hostConfigurator.SetDescription("Sample Error Service hosted Topshelf");
                hostConfigurator.SetDisplayName("Sample Error Service");
                hostConfigurator.SetServiceName("SampleErrorService");
            });
        }
    }
}

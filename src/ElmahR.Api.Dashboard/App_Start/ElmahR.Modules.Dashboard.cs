[assembly: WebActivator.PostApplicationStartMethod( typeof(ElmahR.Api.Dashboard.App_Start.ElmahR.Modules.Dashboard), "PostStart")]

namespace ElmahR.Api.Dashboard.App_Start.ElmahR.Modules {
    public static class Dashboard {
        public static void PostStart() {
            global::ElmahR.Modules.Dashboard.Modules.Bootstrapper.Bootstrap();
        }
    }
}
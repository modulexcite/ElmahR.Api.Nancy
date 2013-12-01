using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ElmahR.Api.Messages;
using ElmahR.Core;
using ElmahR.Core.Config;
using Microsoft.AspNet.SignalR;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using Hub = ElmahR.Core.Hub;

namespace ElmahR.Api.Nancy {
    public class ErrorSubmitionModule : NancyModule {
        public ErrorSubmitionModule()
            : base("nancyapi") {

            Get["/"] = _ => "Hello World!";

            Post["/submitError"] = p => {
                ErrorSubmitRequest request = this.Bind<ErrorSubmitRequest>();
                return PostError(request);
            };

            Get["/submitError/{ErrorId}/{SourceId}/{InfoUrl}/{Error}"] = p => {
                ErrorSubmitRequest request = this.Bind<ErrorSubmitRequest>();
                return PostError(request);
            };

            Post["/submitErrors"] = p => {
                ErrorListSubmitRequest request = this.Bind<ErrorListSubmitRequest>();
                return PostErrors(request);
            };
            Get["/submitErrors"] = p => {
                ErrorListSubmitRequest request = this.Bind<ErrorListSubmitRequest>();
                return PostErrors(request);
            };

        }

        private static ErrorSubmitResponse PostErrors(ErrorListSubmitRequest request) {
            if (request.Errors.Any(error => !AddError(error))) {
                return new ErrorSubmitResponse {
                    SubmitionStatus = 2,
                    SubmitionStatusDescription = "FAILED"
                };
            }
            return new ErrorSubmitResponse {
                SubmitionStatus = 1,
                SubmitionStatusDescription = "OK"
            };
        }

        private static ErrorSubmitResponse PostError(ErrorSubmitRequest request) {
            if (AddError(request)) {
                return new ErrorSubmitResponse {
                    SubmitionStatus = 1,
                    SubmitionStatusDescription = "OK"
                };
            }

            return new ErrorSubmitResponse {
                SubmitionStatus = 2,
                SubmitionStatusDescription = "FAILED"
            };
        }

        private static bool AddError(ErrorSubmitRequest request) {
            var errorText = Decode(request.Error);
            var errorId = request.ErrorId;
            var sourceId = request.SourceId;
            var infoUrl = request.InfoUrl;

            var infoUrlPath = string.IsNullOrWhiteSpace(infoUrl) ? string.Empty : new Uri(infoUrl).AbsoluteUri;

            var section = ConfigurationManager.GetSection("elmahr") as RootSection;
            var hasSection = section != null;
            if (!hasSection) return false;

            var error = (from _ in new[] { errorText }
                         let hasSource = section.Applications.Any(a => a.SourceId == sourceId)
                         let secret = hasSource ? section.Applications.Single(a => a.SourceId == sourceId).Secret : string.Empty
                         let hasSecret = !string.IsNullOrWhiteSpace(secret)
                         select hasSecret ? Crypto.DecryptStringAes(errorText, secret) : errorText)
                .FirstOrDefault();

            var payload = Error.ProcessAndAppendError(sourceId, errorId, error, infoUrlPath, ErrorCatcher(infoUrlPath), true);

            if (payload != null) {
                Hub.Log(string.Format("Received error from {0}: {1}", payload.GetApplication().ApplicationName, payload.Message));
            }

            return true;
        }

        private static bool AddError(ErrorDto errorDto) {
            var errorId = errorDto.ErrorId;
            var sourceId = errorDto.SourceId;
            var infoUrl = errorDto.InfoUrl;

            var infoUrlPath = string.IsNullOrWhiteSpace(infoUrl) ? string.Empty : new Uri(infoUrl).AbsoluteUri;

            var section = ConfigurationManager.GetSection("elmahr") as RootSection;
            var hasSection = section != null;
            if (!hasSection) return false;

            var apps = GlobalHost.DependencyResolver.Resolve<IApplications>();

            var source = apps[sourceId];
            if (source == null) {
                apps.AddSource("Name", sourceId, "", new SourceSection("Name", sourceId, "", null, new Dictionary<string, string>()));
                source = apps[sourceId];
            }

            var error = errorDto.ToError();

            string errorJson = JsonConvert.SerializeObject(error);

            var payload = Error.ProcessAndAppendError(sourceId, errorId, errorJson, infoUrlPath, ErrorCatcher(infoUrlPath), true);

            if (payload != null) {
                Hub.Log(string.Format("Received error from {0}: {1}", payload.GetApplication().ApplicationName, payload.Message));
            }

            return true;
        }

        private static Action<Exception> ErrorCatcher(string infoUrlPath) {
            return ex => {
                Hub.Log(string.Format("An error occurred in ElmahR: {0}", ex), Hub.Severity.Critical);
                Error.ProcessError(Error.ElmahRSourceId, ex, infoUrlPath, _ => { }, true);
            };
        }

        private static string Decode(string str) {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }
    }
}
using System;
using System.Collections.Generic;
using ElmahR.Api.Messages;

namespace ElmahR.ConsoleSource {
    public class SampleRequestFixture {
        public static ErrorListSubmitRequest CreateErrorListSubmitRequest(string winServiceSource = "", string detail = "") {
            var errors = new List<ErrorDto>();
            var error = new ErrorDto();
            error.ErrorId = Guid.NewGuid().ToString();
            error.Source = string.IsNullOrEmpty(winServiceSource) ? "SomeCode" : winServiceSource;
            error.SourceId = "SomeCode";
            error.Type = "ExtrangeException";
            error.Detail = string.IsNullOrEmpty(detail) ? "Error Detail" : detail;
            error.Form = new Dictionary<string, string> { { "exampleFormKey", "exampleFormData" } };
            error.Host = "TheHost";
            error.InfoUrl = "";
            error.Message = "Everything is Lost";
            error.ServerVariables = new Dictionary<string, string> { { "exampleServerKey", "exampleServerData" } };
            error.StatusCode = "404";
            error.Time = DateTime.Now;
            error.User = "TheUser";
            error.WebHostHtmlMessage = "<b>Ugly Error Message</b>";
            error.Cookies = new Dictionary<string, object> { { "exampleCookieKey", "exampleCookieData" } };
            errors.Add(error);
            return new ErrorListSubmitRequest { Errors = errors };
        }
    }
}
using ElmahR.Api.Messages;
using ElmahR.Core;

namespace ElmahR.Api.Nancy {
    public static class MappingExtensions {
        public static Error ToError(this ErrorDto errorDto) {
            return new Error {
                Id = errorDto.ErrorId,
                Host = errorDto.Host,
                Type = errorDto.Type,
                Message = errorDto.Message,
                Detail = errorDto.Detail,
                User = errorDto.User,
                StatusCode = errorDto.StatusCode,
                Time = errorDto.Time,
                WebHostHtmlMessage = errorDto.WebHostHtmlMessage,
                Source = errorDto.SourceId,
                ServerVariables = errorDto.ServerVariables,
                Form = errorDto.Form,
                Cookies = errorDto.Cookies
            };
        }
    }
}
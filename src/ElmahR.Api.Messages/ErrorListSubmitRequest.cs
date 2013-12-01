using System.Collections.Generic;

namespace ElmahR.Api.Messages {
    public class ErrorListSubmitRequest {
        public List<ErrorDto> Errors { get; set; }
    }
}
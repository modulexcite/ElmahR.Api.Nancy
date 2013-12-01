namespace ElmahR.Api.Messages {
    public class ErrorSubmitRequest {
        public string ErrorId { get; set; }
        public string SourceId { get; set; }
        public string InfoUrl { get; set; }
        public string Error { get; set; }
    }
}
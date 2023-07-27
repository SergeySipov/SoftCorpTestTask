namespace WebApi.ViewModel;

public class ErrorDetailsViewModel
{
    public int StatusCode { get; set; }
    public string Error { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
}
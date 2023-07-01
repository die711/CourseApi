using System.Net;

namespace CourseApi.Dto;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSucefull { get; set; } = true;
    public object Result { get; set; }
    public string ErrorMessage { get; set; }
}
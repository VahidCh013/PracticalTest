using System.Net;

namespace PracticalTest.Common.Errors;

public class NotFoundUserException:Exception,IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => "User name or password is wrong";
}
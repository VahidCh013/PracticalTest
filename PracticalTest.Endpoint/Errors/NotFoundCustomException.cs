namespace PracticalTest.Endpoint.Errors;

public class NotFoundCustomException:Exception
{
    public NotFoundCustomException():base(){}

    public NotFoundCustomException(string message):base(message){}

}
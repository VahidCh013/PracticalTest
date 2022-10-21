using Microsoft.AspNetCore.Mvc;

namespace PracticalTest.Endpoint.Controllers;


public class ErrorsController:ControllerBase
{
    [Route("/error")]
    [HttpGet]
    public IActionResult Error()
    {
        return Problem();
    }
}
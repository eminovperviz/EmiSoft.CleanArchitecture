using EmiSoft.CleanArchitecture.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EmiSoft.CleanArchitecture.Controllers;
public class TestController : BaseController
{
    [HttpGet("List")]
    public IActionResult List()
    {
        return Ok(new List<string>
        {
            "Test",
            "Test1",
            "Test2",
        });
    }
}

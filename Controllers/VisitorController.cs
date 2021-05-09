using Microsoft.AspNetCore.Mvc;
using NuxtSignalRApi.Models;

namespace NuxtSignalRApi.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class VisitorController : ControllerBase
    {
        [HttpGet ("count")]
        public VisitorCountModel Count ()
        {
            return new VisitorCountModel () { Counter = 10, Message = "test" };
        }
    }
}
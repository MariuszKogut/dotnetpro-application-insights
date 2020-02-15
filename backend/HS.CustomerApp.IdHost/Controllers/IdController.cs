using HS.CustomerApp.IdHost.Logic;
using Microsoft.AspNetCore.Mvc;

namespace HS.CustomerApp.IdHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdController : ControllerBase
    {
        private readonly IIdService _idService;

        public IdController(IIdService idService)
        {
            _idService = idService;
        }

        [HttpGet]
        public long Generate() => _idService.Generate();
    }
}
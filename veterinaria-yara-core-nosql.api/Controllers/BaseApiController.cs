using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace veterinaria_yara_core_nosql.api.Controllers
{
    //[Authorize]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
    }
}

using Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
   [Route("api/[controller]")]
   [TypeFilter(typeof(ValidationActionFilter))]
   public class ControllerBaseAPI : ControllerBase
   {
   }
}
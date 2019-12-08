using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BackendCore.Attributes;
using Microsoft.AspNetCore.Mvc;
using BackendCore.Models.Account;
using Microsoft.AspNetCore.Cors;

namespace BackendCore.Controllers.Account
{
    [EnableCors]
    [Route("Register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        [ValidateModelState]
        public async Task<JsonResult> Post([FromBody] RegisterUserModel user,CancellationToken token)
        {
            var emailRegexp = new Regex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
            if(!(emailRegexp.IsMatch(user.Email) && emailRegexp.Match(user.Email).Value == user.Email))
            {
                BadRequest();
                return new JsonResult("There was something wrong with model");
            }
            BadRequest();
            return new JsonResult("There was something wrong with model");
        }
    }
}
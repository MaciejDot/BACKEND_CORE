using System.Threading;
using System.Threading.Tasks;
using BackendCore.Models.Account;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BackendCore.Security.Services;
using Microsoft.Extensions.Primitives;
using System.Linq;
using BackendCore.DataTransferObjects.Account.Token;
using BackendCore.Security.Models;
using System;

namespace BackendCore.Controllers.Account
{
    [EnableCors]
    [Route("Token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;

        public TokenController(IUserService userService)
        {
            _userService= userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<TokenDTO>> Get(CancellationToken token)
        {
            var id = User.Claims.Single(x => x.Type == "Id").Value;
            return new TokenDTO { Token = (await _userService.GetTokenForUser(id)).Token };
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TokenDTO>> Post([FromBody]AuthenticationModel userModel, CancellationToken cancellationToken)
        {
            var token = string.Empty;
            try 
            {
                token = (await _userService.Authenticate(userModel)).Token;
            }
            catch
            {
                return Unauthorized();
            }
            return new TokenDTO { Token = token };
        }
    }
}
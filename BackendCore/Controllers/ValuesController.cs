using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCore.Attributes;
using BackendCore.Security.Models;
using BackendCore.Security.Roles;
using BackendCore.Security.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendCore.Controllers
{
    [Route("Values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public static IMediator mediator;
        private readonly IUserService userService;
        public ValuesController(IMediator _mediator, IUserService _userService)
        {
            mediator = _mediator;
            userService = _userService;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
       // [Authorize(Roles = Role.Admin)]
       [Authorize]
        public void Post()
        {
            var user = User;
            return;
        }

        // PUT api/values/5
        [HttpPut]
        public ActionResult<User> Put()
        {
            return userService.Authenticate(new Security.Models.AuthenticationModel { Email = "maciejd0@op.pl" }).Result;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

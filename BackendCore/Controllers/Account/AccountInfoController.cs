﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BackendCore.Controllers.Account
{
    [EnableCors]
    [Route("AccountInfo")]
    [ApiController]
    [Authorize]
    public class AccountInfoController : ControllerBase
    {
        [HttpGet]
        public async Task<JsonResult> Get(CancellationToken token)
        {
            return new JsonResult(new { UserName = User.Claims.Single(x=>x.Type=="Name").Value });
        }
    }
}
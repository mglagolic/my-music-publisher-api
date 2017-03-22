using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.Controllers
{
    public class ApiController : Controller
    {
        [Authorize, HttpGet("~/api/test")]
        public IActionResult GetMessage()
        {
            return Json(new
            {
                Subject = User.GetClaim(OpenIdConnectConstants.Claims.Subject),
                Name = User.Identity.Name
            });
        }
    }
}

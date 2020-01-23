using TODO.Model.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace TODO.WebApi.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        #region Constructor

        public BaseApiController()
        {
        }

        #endregion

        #region Response Methods

        internal ObjectResult InternalServerError(Exception exception,
            [CallerFilePath] string fileName = "",
            [CallerMemberName] string methodName = "")
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
        }

        internal IActionResult ExpectationFailedResponse(bool response)
        {
            if (response)
            {
                return Ok(response);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        /// <summary>
        /// To get Claims
        /// </summary>
        protected User GetClaims()
        {
            var loggedInUserDetails = new User();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                loggedInUserDetails.Email = identity.FindFirst(ClaimTypes.Email).Value;
                loggedInUserDetails.Id = Convert.ToInt32(identity.FindFirst("UserId").Value);
            }

            return loggedInUserDetails;
        }

        #endregion
    }
}
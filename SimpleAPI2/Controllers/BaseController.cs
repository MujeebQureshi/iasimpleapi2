using SimpleAPI2.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleAPI2.Controllers
{
    public class BaseController : ApiController
    {
        public IHttpActionResult ApiResponse(object response, bool hasData = false)
        {
            if (response.GetType() == typeof(Exception) || response.GetType().Name.ToLower().Contains(AppConstants.Exception))
            {
                return BadRequest((response as Exception).Message);
            }
            else if (hasData == false)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(response);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SecuredSwashbuckleExample.Repositories;
using SecuredSwashbuckleExample.Models;
using Microsoft.AspNet.Identity;

namespace SecuredSwashbuckleExample.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository _repo = null;

        public AccountController()
        {
            _repo = new AuthRepository();
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(User userModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            IdentityResult result = await _repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if(errorResult != null)
            {
                return errorResult;
            }

            return Ok();

        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                _repo.Dispose();
            }
 	        base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if(result == null)
            {
                return null;
            }

            if(!result.Succeeded)
            {
                if(result.Errors != null)
                {
                    foreach(string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
                if(ModelState.IsValid)
                {
                    return BadRequest();
                }
                return BadRequest(ModelState);
            }
            return null;
        }
    }
}
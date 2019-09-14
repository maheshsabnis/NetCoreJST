using Core_AppJWT.Models;
using Core_AppJWT.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core_AppJWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        AuthenticationService authenticationService;
        public AuthenticationController(AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }


        [HttpPost]
        public IActionResult Register(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                var IsCreated = authenticationService.RegisterUser(user);
                if (IsCreated == false)
                {
                    return Conflict("The User Already Present");
                }
                var ResponseData = new ResponseData()
                {
                     Message = $"{user.Email} User Created Successfully"
                };
                return Ok(ResponseData);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IActionResult Login(LoginUser inputModel)
        {
            if (ModelState.IsValid)
            {
                var token = authenticationService.Authenticate(inputModel);
                if (token == null)
                {
                    return Unauthorized("The Authentication Failed");
                }
                var ResponseData = new ResponseData()
                {
                    Message = token
                };
                return Ok(ResponseData);
            }
            return BadRequest(ModelState);
        }


    }
}
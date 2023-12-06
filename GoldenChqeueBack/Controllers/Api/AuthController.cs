using GoldenChequeBack.Domain.Auth;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IAccountService _accountService;

        public AuthController( ITokenRepository tokenRepository, IAccountService accountService)
        {
            this._tokenRepository = tokenRepository;
            this._accountService = accountService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO request)
        {
            //Check Email
             var identityUser = await _userManager.FindByEmailAsync(request.Email);
            if (identityUser is not null )
            {
                //Check password
                var checkPasswordResult =  await _userManager.CheckPasswordAsync(identityUser, request.Password);
                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);

                    var jwtToken = _tokenRepository.CreateJWTToken(identityUser, roles.ToList());
                    // create a token and response
                    var response = new LoginResponseDTO()
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = jwtToken
                    };

                    return Ok(response);
                }
                ModelState.AddModelError("", "Email Or Password Is Inccorect");
            }
            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAsync(request, origin));
            ////create IdentityUser object
            //var user = new IdentityUser
            //{
            //    UserName = request.Email?.Trim(),
            //    Email = request.Email?.Trim()
            //};
            ////create user
            //var identityResult = await _userManager.CreateAsync(user,request.Password);
            //if(identityResult.Succeeded)
            //{
            //    //add Role to User("reader")
            //    identityResult =  await _userManager.AddToRoleAsync(user, "Reader");
            //    if (identityResult.Succeeded)
            //    {
            //        return Ok();
            //    }
            //    else
            //    {
            //        if (identityResult.Errors.Any())
            //        {
            //            foreach (var error in identityResult.Errors)
            //            {
            //                ModelState.AddModelError("", error.Description);
            //            }

            //        }

            //    }
            //}
            //else
            //{
            //    if (identityResult.Errors.Any())
            //    {
            //        foreach (var error in identityResult.Errors)
            //        {
            //            ModelState.AddModelError("", error.Description);
            //        }

            //    }

            //}
            //return ValidationProblem(ModelState);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCentre.Models;

namespace WebCentre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpPost("add-user")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                PasswordHash = model.Password,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Adresse = model.Adresse,
                Tel = model.Tel,
                Role = "Professeur"
            };

            var result = await userManager.CreateAsync(user, user.PasswordHash!);

           
         
           
                if (result.Succeeded)
                {
                    return Ok("Registration made successfully");
                }
           else
                {
                    return NotFound("8alta");
                }
            
          
         
              
           
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var signInResult = await signInManager.PasswordSignInAsync(
                  userName: model.Email!,
                  password: model.Password!,
                  isPersistent: false,
                  lockoutOnFailure: false
                  );
            if (signInResult.Succeeded)
            {

                return Ok("register succeed");
            }
            return BadRequest("Error occured");
        }

        //test simple post request

        [HttpPost("log")]
        public async Task<IActionResult> Log(LoginModel model)
        {
            // Validate the login credentials
            if (IsValidLogin(model.Email, model.Password))
            {
                return Ok(new { message = "Login successful" });
            }
            else
            {
                return BadRequest(new { message = "Invalid email or password" });
            }
        }

        private bool IsValidLogin(string email, string password)
        {
            // Your validation logic here
            // For demonstration purposes, accept any email and password combination
            return true;
        }

        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
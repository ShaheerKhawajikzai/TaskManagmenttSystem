using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Managment_System.Data;
using Task_Managment_System.Helper;
using Task_Managment_System.Models;

namespace Task_Managment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly APIResponse<User> _apiResponse;
        public AuthController(ApplicationDbContext dbContext)
        {
            _db = dbContext;
            _apiResponse = new();
        }
        [HttpPost]
        public IActionResult Login([FromBody] User model)
        {

            var userFromDB = _db.Users.FirstOrDefault(x => x.Name == model.Name && model.Password == x.Password);

            if (userFromDB == null)
            {
                _apiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                _apiResponse.ErrorMessages.Add("User Not Found");
                _apiResponse.IsSuccess = false;

                return BadRequest(_apiResponse);
            }
            if (model.Name == model.Name && model.Password == model.Password)
            {
                var token = JwtHelper.GenerateJwtToken(model.Name);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }
    }
}

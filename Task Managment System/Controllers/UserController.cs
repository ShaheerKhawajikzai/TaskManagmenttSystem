using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Managment_System.Data;
using Task_Managment_System.Models;

namespace Task_Managment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly APIResponse<User> _apiResponse;
        public UserController(ApplicationDbContext dbContext)
        {
            _db = dbContext;
            _apiResponse = new();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register(User user)
        {

            if (ModelState.IsValid)
            {

                _db.Users.Add(user);
                _db.SaveChanges();

                return Ok(_apiResponse);
            }
            _apiResponse.ErrorMessages.Add("Model State is invalid.");

            return BadRequest(_apiResponse);
        }

    }
}

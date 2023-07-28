using Microsoft.AspNetCore.Mvc;
using RESTApi.Schemas;
using RESTApi.Services;

namespace RESTApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(List<UserSchemaResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<UserSchemaResponse>> ListUsers()
        {
            try
            {
                var response = _userService.List();
                return Ok(response);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type=typeof(UserSchemaResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserSchemaResponse> CreateUser([FromBody] UserSchemaCreate user)
        {
            try
            {
                var response = _userService.Add(user);
                return Created($"/api/User/{response.Id}", response);
            } catch (Exception _) {
                return BadRequest();
            }
        }
    }
}

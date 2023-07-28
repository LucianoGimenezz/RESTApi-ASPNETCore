using DB;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using RESTApi.Schemas;
using RESTApi.Services;

namespace RESTApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(List<Todo>))]
        public ActionResult<List<Todo>> ListTodos()
        {
            try
            {
                var response = _todoService.GetTodos();
                return response;

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return BadRequest("Unexpected Error");
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoSchemaResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetTodoById(int id) {
            TodoSchemaResponse response = null;
            try
            {
                response = _todoService.GetTodoById(id);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                if (ex.Message == "Not found")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type =typeof(Todo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateTodo([FromBody] TodoSchemaBase todo) {
            Todo response = null;
            try
            {
               response = _todoService.AddTodo(todo);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
            return Created($"/Todo/{response.Id}", response);
        }
    }
}

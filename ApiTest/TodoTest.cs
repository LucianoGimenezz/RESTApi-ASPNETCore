using DB;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using RESTApi.Controllers;
using RESTApi.Schemas;
using RESTApi.Services;

namespace ApiTest
{
    public class TodoTest
    {
        private readonly TodoController _todoController;
        private readonly ITodoService _todoService;

        public TodoTest()
        {
            var mockContext = new Mock<ITodoContext>();
            _todoService = new TodoService(mockContext.Object);
            _todoController = new TodoController(_todoService);
        }

        [Fact]
        public void Get_Ok()
        {
            var result = _todoController.ListTodos();
            Assert.IsType<ActionResult<List<Todo>>>(result);
        }

        [Fact]
        public void Get_By_Id_Ok()
        {
            int id = 1;
            var options = new DbContextOptionsBuilder<TodoContext>()
                               .UseInMemoryDatabase(databaseName:"TodoDatabase")
                               .Options;
  

            using (var context = new TodoContext(options))
            {
                context.Todos.Add(new Todo { Id = id, Title = "test", Description = "test" });
                context.SaveChanges();
            }

            using (var context = new TodoContext(options))
            {
                var service = new TodoService(context);
                var controller = new TodoController(service);
                var result = controller.GetTodoById(id);

                
                Assert.IsType<OkObjectResult>(result);

                var castResult = (OkObjectResult)result;

                var value = Assert.IsType<TodoSchemaResponse>(castResult?.Value);
                Assert.NotNull(value);
                Assert.Equal(id, value.Id);
            }
        }

        [Fact]
        public void Get_By_Id_Not_found()
        {
            int id = 1;
            var options = new DbContextOptionsBuilder<TodoContext>()
                             .UseInMemoryDatabase(databaseName: "TodoDatabase")
                             .Options;


            using (var context = new TodoContext(options))
            {
                context.Todos.Add(new Todo { Id = id, Title = "test", Description = "test" });
                context.SaveChanges();
            }

            using (var context = new TodoContext(options))
            {
                var service = new TodoService(context);
                var controller = new TodoController(service);
                var result = controller.GetTodoById(2);

                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
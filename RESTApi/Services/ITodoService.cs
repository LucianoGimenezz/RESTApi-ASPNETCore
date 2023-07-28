using DB.Models;
using RESTApi.Schemas;

namespace RESTApi.Services
{
    public interface ITodoService
    {

        public List<Todo> GetTodos();
        public Todo AddTodo(TodoSchemaBase todo);
        public TodoSchemaResponse GetTodoById(int id);
    }
}

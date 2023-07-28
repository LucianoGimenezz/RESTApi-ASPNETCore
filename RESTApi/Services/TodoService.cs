using DB;
using DB.Models;
using RESTApi.Schemas;

namespace RESTApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoContext _todoContext;

        public TodoService(ITodoContext todoContext)
        {
            _todoContext = todoContext;
        }


        public List<Todo> GetTodos(){
            try
            {
                var response = _todoContext.Todos.Select(t => t).ToList();
                return response;

            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public TodoSchemaResponse GetTodoById(int  id)
        {
            var query = from t in _todoContext.Todos
                        where t.Id == id
                        select t;
            var todo = query.FirstOrDefault();

            if (todo == null)
            {
                throw new Exception("Not found");
            }

            TodoSchemaResponse response = new TodoSchemaResponse { Id = todo.Id, Title = todo.Title, Description = todo.Description, IsDone = todo.IsDone };

            return response;
        }
        public Todo AddTodo(TodoSchemaBase todo)
        {
            try
            {
               var response = _todoContext.Todos.Add(new Todo
               {
                   Title = todo.Title,
                   Description = todo.Description,
                   UserId = todo.UserId,
               });
               _todoContext.SaveChanges();

               return response.Entity;
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

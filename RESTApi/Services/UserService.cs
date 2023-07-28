using DB;
using DB.Models;
using RESTApi.Schemas;

namespace RESTApi.Services
{
    public class UserService: IUserService
    {
        private readonly TodoContext _todoContext;

        public UserService(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }
        public List<UserSchemaResponse> List()
        {
            try
            {
                var response = _todoContext.Users.Select(u => u).ToList();
                var usersSerialaizer = response.Select(u => new UserSchemaResponse {
                    Id = u.Id, 
                    Email = u.Email, 
                    UserName = u.UserName, 
                    Todos = (List<Todo>)u.Todos 
                }).ToList();

                return usersSerialaizer;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserSchemaResponse Add(UserSchemaCreate user)
        {
            try
            {
                User newUser = new()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    // se debe de hashear
                    Password = user.Password,
                };

                var result = _todoContext.Users.Add(newUser);
                _todoContext.SaveChanges();
                List<Todo>? todos = result.Entity.Todos == null ? null : (List<Todo>)result.Entity.Todos;
                UserSchemaResponse responseUser = new()
                {
                    Id = result.Entity.Id,
                    Email = result.Entity.Email,
                    UserName = result.Entity.UserName,
                    Todos = todos
                };
                return responseUser;

            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

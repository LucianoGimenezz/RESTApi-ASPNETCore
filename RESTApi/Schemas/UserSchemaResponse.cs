using DB.Models;

namespace RESTApi.Schemas
{
    public class UserSchemaResponse: UserSchemaBase
    {
        public int Id { get; set; }
        public List<Todo> Todos { get; set; }
    }
}

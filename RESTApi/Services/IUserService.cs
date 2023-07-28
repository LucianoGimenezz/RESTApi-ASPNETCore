using DB.Models;
using RESTApi.Schemas;

namespace RESTApi.Services
{
    public interface IUserService
    {

        public UserSchemaResponse Add(UserSchemaCreate user);
        public List<UserSchemaResponse> List();
    }
}

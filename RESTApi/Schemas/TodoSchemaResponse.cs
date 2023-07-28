namespace RESTApi.Schemas
{
    public class TodoSchemaResponse: TodoSchemaBase
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
    }
}

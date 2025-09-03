namespace FieldManagementSystem.Entites
{
    public class Field
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Guid UserId { get; set; }
    }
}

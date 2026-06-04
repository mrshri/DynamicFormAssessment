namespace DynamicForm.Domain.Entities
{
    public class Form
    {
        public int Id { get; set; }

        public string FormKey { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public int Version { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<FormField> Fields { get; set; } = new List<FormField>();
    }
}

namespace DynamicForm.Domain.Entities
{
    public class FieldOption
    {
        public int Id { get; set; }

        public int FormFieldId { get; set; }

        public string OptionLabel { get; set; } = string.Empty;

        public string OptionValue { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public FormField FormField { get; set; } = null!;
    }
}

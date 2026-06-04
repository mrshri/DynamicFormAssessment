namespace DynamicForm.Domain.Entities
{
    public class FormField
    {
        public int Id { get; set; }

        public int FormId { get; set; }

        public string FieldName { get; set; } = string.Empty;

        public string Label { get; set; } = string.Empty;

        public string InputType { get; set; } = string.Empty;

        public string? Placeholder { get; set; }

        public string? DefaultValue { get; set; }

        public bool IsRequired { get; set; }

        public int DisplayOrder { get; set; }

        public int ColumnSpan { get; set; } = 1;

        public bool IsActive { get; set; } = true;

        public Form Form { get; set; } = null!;

        public ICollection<FieldValidation> Validations { get; set; } = new List<FieldValidation>();

        public ICollection<FieldOption> Options { get; set; } = new List<FieldOption>();
    }
}

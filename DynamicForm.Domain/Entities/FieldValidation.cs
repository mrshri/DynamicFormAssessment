namespace DynamicForm.Domain.Entities
{
    public class FieldValidation
    {
        public int Id { get; set; }

        public int FormFieldId { get; set; }

        public string ValidationType { get; set; } = string.Empty;

        public string? ValidationValue { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public FormField FormField { get; set; } = null!;
    }
}

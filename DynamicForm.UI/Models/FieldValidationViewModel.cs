namespace DynamicForm.UI.Models
{
    public class FieldValidationViewModel
    {
        public string ValidationType { get; set; } = string.Empty;
        public string? ValidationValue { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}

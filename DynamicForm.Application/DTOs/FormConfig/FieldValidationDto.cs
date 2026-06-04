namespace DynamicForm.Application.DTOs.FormConfig
{
    public class FieldValidationDto
    {
        public string ValidationType { get; set; } = string.Empty;
        public string? ValidationValue { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}

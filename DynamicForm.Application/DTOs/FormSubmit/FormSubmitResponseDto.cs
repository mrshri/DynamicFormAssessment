namespace DynamicForm.Application.DTOs.FormSubmit
{
    public class FormSubmitResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public Dictionary<string, List<string>> Errors { get; set; } = new();
    }
}

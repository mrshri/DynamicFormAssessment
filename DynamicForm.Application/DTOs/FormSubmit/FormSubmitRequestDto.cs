namespace DynamicForm.Application.DTOs.FormSubmit
{
    public class FormSubmitRequestDto
    {
        public string FormKey { get; set; } = string.Empty;

        public int FormVersion { get; set; }

        public Dictionary<string, string?> Data { get; set; } = new();
    }
}

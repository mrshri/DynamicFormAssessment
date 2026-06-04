namespace DynamicForm.Application.DTOs.FormConfig
{
    public class FormConfigResponseDto
    {
        public string FormKey { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Version { get; set; }

        public List<FormFieldDto> Fields { get; set; } = new();
    }
}

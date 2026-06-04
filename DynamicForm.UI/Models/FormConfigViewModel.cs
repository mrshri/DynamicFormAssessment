namespace DynamicForm.UI.Models
{
    public class FormConfigViewModel
    {
        public string FormKey { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Version { get; set; }

        public List<FormFieldViewModel> Fields { get; set; } = new();
    }
}

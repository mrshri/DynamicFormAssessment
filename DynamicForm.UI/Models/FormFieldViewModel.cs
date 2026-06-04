namespace DynamicForm.UI.Models
{
    public class FormFieldViewModel
    {
        public string FieldName { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string InputType { get; set; } = string.Empty;
        public string? Placeholder { get; set; }
        public string? DefaultValue { get; set; }

        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public int ColumnSpan { get; set; }

        public List<FieldValidationViewModel> Validations { get; set; } = new();
        public List<FieldOptionViewModel> Options { get; set; } = new();
    }
}

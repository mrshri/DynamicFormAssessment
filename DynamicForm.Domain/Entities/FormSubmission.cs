namespace DynamicForm.Domain.Entities
{
    public class FormSubmission
    {
        public int Id { get; set; }

        public int FormId { get; set; }

        public int FormVersion { get; set; }

        public string SubmittedDataJson { get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public Form Form { get; set; } = null!;
    }
}

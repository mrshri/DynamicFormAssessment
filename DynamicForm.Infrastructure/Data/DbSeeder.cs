using DynamicForm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicForm.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static  void Seed(AppDbContext context)
        {
             context.Database.Migrate();

                if (context.Forms.Any(x => x.FormKey == "claim-details"))
                   return;

            var form = new Form
            {
                FormKey = "claim-details",
                Title = "CLAIM DETAILS",
                Version = 1,
                IsActive = true,
                Fields = new List<FormField>
            {
                new()
                {
                    FieldName = "imageNo",
                    Label = "Image No",
                    InputType = "textbox",
                    DefaultValue = "IMG-2024-0042",
                    IsRequired = true,
                    DisplayOrder = 1,
                    ColumnSpan = 1,
                    Validations = new List<FieldValidation>
                    {
                        new() { ValidationType = "required", ErrorMessage = "Image No is required" },
                        new() { ValidationType = "maxLength", ValidationValue = "20", ErrorMessage = "Image No cannot exceed 20 characters" }
                    }
                },
                new()
                {
                    FieldName = "taxId",
                    Label = "Tax ID",
                    InputType = "textbox",
                    Placeholder = "Enter Tax ID",
                    IsRequired = true,
                    DisplayOrder = 2,
                    ColumnSpan = 1,
                    Validations = new List<FieldValidation>
                    {
                        new() { ValidationType = "required", ErrorMessage = "Tax ID is required" },
                        new() { ValidationType = "numeric", ErrorMessage = "Tax ID must contain only numbers" }
                    }
                },
                new()
                {
                    FieldName = "providerType",
                    Label = "Provider Type",
                    InputType = "dropdown",
                    IsRequired = true,
                    DisplayOrder = 3,
                    ColumnSpan = 1,
                    Options = new List<FieldOption>
                    {
                        new() { OptionLabel = "Non PAR", OptionValue = "Non PAR", DisplayOrder = 1 },
                        new() { OptionLabel = "PAR", OptionValue = "PAR", DisplayOrder = 2 }
                    },
                    Validations = new List<FieldValidation>
                    {
                        new() { ValidationType = "required", ErrorMessage = "Provider Type is required" }
                    }
                },
                new()
                {
                    FieldName = "state",
                    Label = "State",
                    InputType = "dropdown",
                    DefaultValue = "Karnataka",
                    IsRequired = false,
                    DisplayOrder = 4,
                    ColumnSpan = 1,
                    Options = new List<FieldOption>
                    {
                        new() { OptionLabel = "Karnataka", OptionValue = "Karnataka", DisplayOrder = 1 },
                        new() { OptionLabel = "Maharashtra", OptionValue = "Maharashtra", DisplayOrder = 2 },
                        new() { OptionLabel = "Delhi", OptionValue = "Delhi", DisplayOrder = 3 }
                    }
                },
                new()
                {
                    FieldName = "remarks",
                    Label = "Remarks",
                    InputType = "textarea",
                    Placeholder = "Optional notes...",
                    IsRequired = false,
                    DisplayOrder = 5,
                    ColumnSpan = 2
                },
                new()
                {
                    FieldName = "outcome",
                    Label = "Outcome",
                    InputType = "dropdown",
                    DefaultValue = "Complete",
                    IsRequired = true,
                    DisplayOrder = 6,
                    ColumnSpan = 1,
                    Options = new List<FieldOption>
                    {
                        new() { OptionLabel = "Complete", OptionValue = "Complete", DisplayOrder = 1 },
                        new() { OptionLabel = "Pending", OptionValue = "Pending", DisplayOrder = 2 },
                        new() { OptionLabel = "Rejected", OptionValue = "Rejected", DisplayOrder = 3 }
                    },
                    Validations = new List<FieldValidation>
                    {
                        new() { ValidationType = "required", ErrorMessage = "Outcome is required" }
                    }
                },
                new()
                {
                    FieldName = "assignedTo",
                    Label = "Assigned To",
                    InputType = "dropdown",
                    Placeholder = "Select user",
                    IsRequired = false,
                    DisplayOrder = 7,
                    ColumnSpan = 1,
                    Options = new List<FieldOption>
                    {
                        new() { OptionLabel = "Rahul", OptionValue = "rahul", DisplayOrder = 1 },
                        new() { OptionLabel = "Amit", OptionValue = "amit", DisplayOrder = 2 },
                        new() { OptionLabel = "Priya", OptionValue = "priya", DisplayOrder = 3 }
                    }
                }
            }
            };

             context.Forms.Add(form);
             context.SaveChanges();
        }
    }
}

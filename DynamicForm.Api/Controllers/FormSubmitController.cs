using DynamicForm.Application.DTOs.FormSubmit;
using DynamicForm.Domain.Entities;
using DynamicForm.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DynamicForm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormSubmitController(AppDbContext _context) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<FormSubmitResponseDto>> SubmitForm(FormSubmitRequestDto request)
        {
            var form = await _context.Forms
                .Include(x => x.Fields.Where(f => f.IsActive))
                    .ThenInclude(x => x.Validations)
                .Include(x => x.Fields.Where(f => f.IsActive))
                    .ThenInclude(x => x.Options)
                .FirstOrDefaultAsync(x =>
                    x.FormKey == request.FormKey &&
                    x.Version == request.FormVersion &&
                    x.IsActive);

            if (form == null)
            {
                return NotFound(new FormSubmitResponseDto
                {
                    Success = false,
                    Message = "Form configuration not found."
                });
            }

            var errors = new Dictionary<string, List<string>>();

            foreach (var field in form.Fields.Where(x => x.IsActive))
            {
                request.Data.TryGetValue(field.FieldName, out var value);

                foreach (var validation in field.Validations)
                {
                    switch (validation.ValidationType)
                    {
                        case "required":
                            if (string.IsNullOrWhiteSpace(value))
                                AddError(errors, field.FieldName, validation.ErrorMessage);
                            break;

                        case "numeric":
                            if (!string.IsNullOrWhiteSpace(value) && !value.All(char.IsDigit))
                                AddError(errors, field.FieldName, validation.ErrorMessage);
                            break;

                        case "maxLength":
                            if (!string.IsNullOrWhiteSpace(value)
                                && int.TryParse(validation.ValidationValue, out var max)
                                && value.Length > max)
                            {
                                AddError(errors, field.FieldName, validation.ErrorMessage);
                            }
                            break;

                        case "regex":
                            if (!string.IsNullOrWhiteSpace(value)
                                && !string.IsNullOrWhiteSpace(validation.ValidationValue)
                                && !Regex.IsMatch(value, validation.ValidationValue))
                            {
                                AddError(errors, field.FieldName, validation.ErrorMessage);
                            }
                            break;
                    }
                }

                if (field.InputType == "dropdown" && !string.IsNullOrWhiteSpace(value))
                {
                    var isValidOption = field.Options.Any(x => x.OptionValue == value);

                    if (!isValidOption)
                        AddError(errors, field.FieldName, $"{field.Label} has invalid selected value.");
                }
            }

            if (errors.Any())
            {
                return BadRequest(new FormSubmitResponseDto
                {
                    Success = false,
                    Message = "Validation failed.",
                    Errors = errors
                });
            }

            var submission = new FormSubmission
            {
                FormId = form.Id,
                FormVersion = form.Version,
                SubmittedDataJson = JsonSerializer.Serialize(request.Data),
                SubmittedAt = DateTime.UtcNow
            };

            await _context.FormSubmissions.AddAsync(submission);
            await _context.SaveChangesAsync();

            return Ok(new FormSubmitResponseDto
            {
                Success = true,
                Message = "Form submitted successfully."
            });
        }

        private static void AddError(
            Dictionary<string, List<string>> errors,
            string fieldName,
            string errorMessage)
        {
            if (!errors.ContainsKey(fieldName))
                errors[fieldName] = new List<string>();

            errors[fieldName].Add(errorMessage);
        }
    }
}

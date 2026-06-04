using DynamicForm.Application.DTOs.FormConfig;
using DynamicForm.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DynamicForm.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FormConfigController(AppDbContext _context) : ControllerBase
    {
        [HttpGet("{formKey}")]
        public async Task<ActionResult<FormConfigResponseDto>> GetFormConfig(string formKey)
        {
            var form = await _context.Forms
                .Include(x => x.Fields.Where(f => f.IsActive))
                    .ThenInclude(x => x.Validations)
                .Include(x => x.Fields.Where(f => f.IsActive))
                    .ThenInclude(x => x.Options)
                .Where(x => x.FormKey == formKey && x.IsActive)
                .OrderByDescending(x => x.Version)
                .FirstOrDefaultAsync();

            if (form == null)
            {
                return NotFound(new
                {
                    message = $"Form config not found for key: {formKey}"
                });
            }

            var response = new FormConfigResponseDto
            {
                FormKey = form.FormKey,
                Title = form.Title,
                Version = form.Version,
                Fields = form.Fields
                    .OrderBy(x => x.DisplayOrder)
                    .Select(field => new FormFieldDto
                    {
                        FieldName = field.FieldName,
                        Label = field.Label,
                        InputType = field.InputType,
                        Placeholder = field.Placeholder,
                        DefaultValue = field.DefaultValue,
                        IsRequired = field.IsRequired,
                        DisplayOrder = field.DisplayOrder,
                        ColumnSpan = field.ColumnSpan,

                        Validations = field.Validations.Select(v => new FieldValidationDto
                        {
                            ValidationType = v.ValidationType,
                            ValidationValue = v.ValidationValue,
                            ErrorMessage = v.ErrorMessage
                        }).ToList(),

                        Options = field.Options
                            .OrderBy(o => o.DisplayOrder)
                            .Select(o => new FieldOptionDto
                            {
                                OptionLabel = o.OptionLabel,
                                OptionValue = o.OptionValue
                            }).ToList()
                    })
                    .ToList()
            };

            return Ok(response);
        }
    }
}

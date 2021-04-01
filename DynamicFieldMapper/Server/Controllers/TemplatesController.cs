using DynamicFieldMapper.Server.Data;
using DynamicFieldMapper.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicFieldMapper.Server.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class TemplatesController : ControllerBase {
    private readonly IDbContextFactory<DataMapContext> contextFactory;
    // TODO: Replace this with claims principle (e.g. User.Identity.Name) in the action instead
    private const string UserName = "myuser";
    public TemplatesController(IDbContextFactory<DataMapContext> contextFactory) {
      this.contextFactory = contextFactory;
    }

    [HttpPost]
    public async Task<IActionResult> SaveTemplate(ImportFile model) {
      var retModel = new ResponseModel<ImportFile>() {
        ReturnObject = model
      };

      // TODO: Validate the ImportFile model

      using var context = contextFactory.CreateDbContext();
      var template = context.Templates.SingleOrDefault(x => x.Name == model.TemplateName && x.UserName == UserName);
      if (template is { }) {
        retModel.AddError($"A template already exists with the name {model.TemplateName}. Please specify a unique name for the template.");
        return Ok(retModel);
      }

      template = new() {
        TemplateFields = new List<TemplateField>(),
        Name = model.TemplateName,
        UserName = UserName
      };

      var mappableFields = context.MappableFields.ToList();

      foreach (var (mappableField, templateField) in model.GetPropertiesWithVals()) {
        var curMapField = mappableFields.Single(x => x.Name == mappableField);
        template.TemplateFields.Add(new() {
          Name = (string)templateField,
          MappableFieldId = curMapField.Id
        });
      }

      using var trans = await context.Database.BeginTransactionAsync();
      try {
        context.Templates.Add(template);
        retModel.RecordCount = await context.SaveChangesAsync();
        await trans.CommitAsync();
      } catch (Exception ex) {
        if (trans is { }) { await trans.RollbackAsync(); }
        retModel.AddError("An error occurred while trying to save the Template to the database.");
#if DEBUG
        while (ex is { }) {
          retModel.AddError(ex.Message);
          if (!string.IsNullOrWhiteSpace(ex.StackTrace)) {
            retModel.AddError(ex.StackTrace);
          }
          ex = ex.InnerException;
        }
#endif
      } finally {
        if (trans is { }) { await trans.DisposeAsync(); }
      }

      return Ok(retModel);
    }

    [HttpPost("Search")]
    public IActionResult SearchTemplates([FromBody] List<string> headers) {
      var retModel = new ResponseModel<List<ImportFile>>() {
        ReturnObject = new List<ImportFile>()
      };
      try {
        using var context = contextFactory.CreateDbContext();
        var templates = context.Templates
          .Include(x => x.TemplateFields)
          .ThenInclude(x => x.MappableField)
          .Where(x => x.UserName == UserName && x.TemplateFields.Any(y =>  headers.Contains(y.Name)));

        foreach (var foundTemplate in templates) {
          if (foundTemplate.TemplateFields.Where(x => headers.Contains(x.Name)).Count() == foundTemplate.TemplateFields.Count) {
            var forAdd = new ImportFile() { TemplateName = foundTemplate.Name };
            foreach (var (mappableField, templateField, setter) in forAdd.GetPropertiesWithValsAndSetter()) {
              setter(foundTemplate.TemplateFields.SingleOrDefault(x => x.MappableField.Name == mappableField)?.Name);
            }
            retModel.ReturnObject.Add(forAdd);
          }
        }
      } catch (Exception ex) {
        retModel.AddError("An error occurred while trying to find matching templates for the provided header names");
#if DEBUG
        while (ex is { }) {
          retModel.AddError(ex.Message);
          if (!string.IsNullOrWhiteSpace(ex.StackTrace)) {
            retModel.AddError(ex.StackTrace);
          }
          ex = ex.InnerException;
        }
#endif
      }

      return Ok(retModel);
    }
  }
}

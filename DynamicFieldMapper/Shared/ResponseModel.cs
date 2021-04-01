using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFieldMapper.Shared {
  public class ResponseModelBase {
    public List<string> ErrorMessages { get; set; } = new List<string>();
    public List<Exception> Exceptions { get; set; } = new List<Exception>();
    public bool WasError => ErrorMessages.Any() || Exceptions.Any();
    public int RecordCount { get; set; }

    public string AsHtml() {
      var outputStr = new StringBuilder();
      if ((ErrorMessages?.Count ?? 0) > 0) {
        outputStr.AppendLine("<ul class=\"resp-error text-danger\">");
        foreach (var curErrMsg in ErrorMessages) {
          outputStr.AppendLine($"  <li>{curErrMsg}</li>");
        }
        outputStr.AppendLine("</ul>");
      }

      if ((Exceptions?.Count ?? 0) > 0) {
        var curExInd = 0;
        foreach (var curEx in Exceptions) {
          outputStr.AppendLine($"<div id=\"ex-{curExInd++}\" class=\"exception-block\">");
          outputStr.AppendLine($"  <p class=\"exception-message\">{curEx.Message}</p>");
          outputStr.AppendLine($"  <pre class=\"stack-trace\">{curEx.StackTrace}</pre>");
          outputStr.AppendLine("</div>");
        }
      }
      return outputStr.ToString();
    }

    public virtual void ClearErrors() {
      ErrorMessages = null;
      Exceptions = null;
    }

    public void AddError(string message) {
      if (ErrorMessages is null) { ErrorMessages = new List<string>(); }
      ErrorMessages.Add(message);
    }

    public void AddError(Exception ex) {
      if (Exceptions is null) { Exceptions = new List<Exception>(); }
      Exceptions.Add(ex);
    }

    public void AddErrors(List<string> messages) {
      if (ErrorMessages is null) { ErrorMessages = new List<string>(); }
      ErrorMessages.AddRange(messages);
    }

    public void AddErrors(List<Exception> exceptions) {
      if (Exceptions is null) { Exceptions = new List<Exception>(); }
      Exceptions.AddRange(exceptions);
    }
  }

  public class ResponseModelBase<TKey> : ResponseModelBase {
    public TKey SavedId { get; set; }
  }

  public class ResponseModel<T> : ResponseModelBase
    where T : class {
    public T ReturnObject { get; set; }
  }

  public class ResponseModel<T, TKey> : ResponseModelBase<TKey>
    where T : class {
    public T ReturnObject { get; set; }
  }
}

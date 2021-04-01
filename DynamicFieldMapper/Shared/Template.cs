using System.Collections.Generic;

namespace DynamicFieldMapper.Shared {
  public partial class Template {
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }

    public virtual ICollection<TemplateField> TemplateFields { get; set; }
  }
}

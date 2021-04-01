using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFieldMapper.Shared {
  public partial class MappableField {
    public int Id { get; set; }
    public string Name { get; set; }
    // Any other fields, such as:
    public TypeCode DataType { get; set; }

    public virtual ICollection<TemplateField> TemplateFields { get; set; }
  }
}

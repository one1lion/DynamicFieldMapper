using System;
using System.ComponentModel.DataAnnotations;

namespace DynamicFieldMapper.Shared {
  public class ImportFileData {
    public string Company { get; set; }
    public string Product { get; set; }
    public double Quantity { get; set; }
    [Display(Name = "Effective Date")]
    public DateTime EffectiveDate { get; set; }
    [Display(Name = "Less Included")]
    public double? LessIncluded { get; set; }
    [Display(Name = "Unit Cost")]
    public double? UnitCost { get; set; }
    [Display(Name = "Unit Price")]
    public double? UnitPrice { get; set; }
    [Display(Name = "Cancel Date")]
    public DateTime? CancelDate { get; set; }
    public double? Sequence { get; set; }
    [Display(Name = "Serial Number")]
    public string SerialNumber { get; set; }
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DynamicFieldMapper.Shared {
  public class ImportFile {
    [Required]
    [MaxLength(60)]
    public string TemplateName { get; set; }
    [Required]
    [MaxLength(60)]
    public string Company { get; set; }
    [Required]
    [MaxLength(60)]
    public string Product { get; set; }
    [Required]
    [MaxLength(60)]
    public string Quantity { get; set; }
    [Display(Name = "Effective Date")]
    [Required]
    [MaxLength(60)]
    public string EffectiveDate { get; set; }
    [Display(Name = "Less Included")]
    [MaxLength(60)]
    public string LessIncluded { get; set; }
    [Display(Name = "Unit Cost")]
    [MaxLength(60)]
    public string UnitCost { get; set; }
    [Display(Name = "Unit Price")]
    [MaxLength(60)]
    public string UnitPrice { get; set; }
    [Display(Name = "Cancel Date")]
    [MaxLength(60)]
    public string CancelDate { get; set; }
    [MaxLength(60)]
    public string Sequence { get; set; }
    [Display(Name = "Serial Number")]
    [MaxLength(60)]
    public string SerialNumber { get; set; }


    // This is if you just want the property value.
    public IEnumerable<object> GetProperties() {
      // this instructs the compiler to return one value and then wait 
      yield return Company;
      yield return Product;
      yield return Quantity;
      yield return EffectiveDate;
      yield return LessIncluded;
      yield return UnitCost;
      yield return UnitPrice;
      yield return CancelDate;
      yield return Sequence;
      yield return SerialNumber;
    }

    // This is if you want to have the name of the property and its value.
    public IEnumerable<(string Name, object Value)> GetPropertiesWithVals() {
      // this instructs the compiler to return one value and then wait 
      yield return (nameof(Company), Company);
      yield return (nameof(Product), Product);
      yield return (nameof(Quantity), Quantity);
      yield return (nameof(EffectiveDate), EffectiveDate);
      yield return (nameof(LessIncluded), LessIncluded);
      yield return (nameof(UnitCost), UnitCost);
      yield return (nameof(UnitPrice), UnitPrice);
      yield return (nameof(CancelDate), CancelDate);
      yield return (nameof(Sequence), Sequence);
      yield return (nameof(SerialNumber), SerialNumber);
    }

    public object GetValue(string forPropName) {
      return GetPropertiesWithVals().SingleOrDefault(x => x.Name == forPropName).Value;
    }

    public IEnumerable<(string Name, object Value, Action<object>)> GetPropertiesWithValsAndSetter() {
      yield return (nameof(Company), Company, newVal => Company = (string)newVal);
      yield return (nameof(Product), Product, newVal => Product = (string)newVal);
      yield return (nameof(Quantity), Quantity, newVal => Quantity = (string)newVal);
      yield return (nameof(EffectiveDate), EffectiveDate, newVal => EffectiveDate = (string)newVal);
      yield return (nameof(LessIncluded), LessIncluded, newVal => LessIncluded = (string)newVal);
      yield return (nameof(UnitCost), UnitCost, newVal => UnitCost = (string)newVal);
      yield return (nameof(UnitPrice), UnitPrice, newVal => UnitPrice = (string)newVal);
      yield return (nameof(CancelDate), CancelDate, newVal => CancelDate = (string)newVal);
      yield return (nameof(Sequence), Sequence, newVal => Sequence = (string)newVal);
      yield return (nameof(SerialNumber), SerialNumber, newVal => SerialNumber = (string)newVal);
    }

    public static IEnumerable<(string, Func<ImportFile, object>, Action<ImportFile, object>)> GetPropertyNamesWithValsAndSetter() {
      yield return (nameof(Company), (model) => model.Company, (model, newVal) => model.Company = (string)newVal);
      yield return (nameof(Product), (model) => model.Product, (model, newVal) => model.Product = (string)newVal);
      yield return (nameof(Quantity), (model) => model.Quantity, (model, newVal) => model.Quantity = (string)newVal);
      yield return (nameof(EffectiveDate), (model) => model.EffectiveDate, (model, newVal) => model.EffectiveDate = (string)newVal);
      yield return (nameof(LessIncluded), (model) => model.LessIncluded, (model, newVal) => model.LessIncluded = (string)newVal);
      yield return (nameof(UnitCost), (model) => model.UnitCost, (model, newVal) => model.UnitCost = (string)newVal);
      yield return (nameof(UnitPrice), (model) => model.UnitPrice, (model, newVal) => model.UnitPrice = (string)newVal);
      yield return (nameof(CancelDate), (model) => model.CancelDate, (model, newVal) => model.CancelDate = (string)newVal);
      yield return (nameof(Sequence), (model) => model.Sequence, (model, newVal) => model.Sequence = (string)newVal);
      yield return (nameof(SerialNumber), (model) => model.SerialNumber, (model, newVal) => model.SerialNumber = (string)newVal);
    }

    // This is if you want to have the name of the property and its value.
    public static IEnumerable<string> GetPropertyNames() {
      // this instructs the compiler to return one value and then wait 
      yield return nameof(Company);
      yield return nameof(Product);
      yield return nameof(Quantity);
      yield return nameof(EffectiveDate);
      yield return nameof(LessIncluded);
      yield return nameof(UnitCost);
      yield return nameof(UnitPrice);
      yield return nameof(CancelDate);
      yield return nameof(Sequence);
      yield return nameof(SerialNumber);
    }
  }
}

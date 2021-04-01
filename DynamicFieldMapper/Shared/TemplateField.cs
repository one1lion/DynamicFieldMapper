namespace DynamicFieldMapper.Shared {
  public partial class TemplateField {
    public int Id { get; set; }
    public string Name { get; set; }
    public int TemplateId { get; set; }
    public int MappableFieldId { get; set; }

    public virtual Template Template { get; set; }
    public virtual MappableField MappableField { get; set; }
  }
}

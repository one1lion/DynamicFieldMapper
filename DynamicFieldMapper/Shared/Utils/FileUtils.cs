using ExcelDataReader;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace DynamicFieldMapper.Shared {
  public static class FileUtils {
    public static async Task<DataTable> ProcessFile(Stream stream, string contentType, string fileName = default) {
      using var memStream = new MemoryStream();
      await stream.CopyToAsync(memStream);
      IExcelDataReader reader = default;
      switch (contentType) {
        case "text/csv":
          reader = ExcelReaderFactory.CreateCsvReader(memStream);
          break;
        //TODO: if open office files are allowed case "application/vnd.oasis.pendocument.spreadsheet": // .ods
        case "application/vnd.ms-excel": // .xls (and sometimes .csv)
        case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": // .xlsx
          if (Path.GetExtension(fileName).ToLower() == ".csv") {
            reader = ExcelReaderFactory.CreateCsvReader(memStream);
            break;
          } else {
            reader = ExcelReaderFactory.CreateReader(memStream);
            break;
          }
        default:
          throw new IOException("The file type is invalid.Please provide an Excel document(.xls or.xlsx) or CSV file");
      }
      var forRet = ProcessRows(reader);
      reader.Close();
      reader.Dispose();
      return forRet;
    }

    public static DataTable ProcessRows(IExcelDataReader reader) {
      var result = reader.AsDataSet(new ExcelDataSetConfiguration() {
        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() {
          UseHeaderRow = true
        }
      });
      DataTable dt = result.Tables[0];
      return dt;
    }
  }

  public static class DataTableExtensions {
    public static string GetValueAsString(this DataRow row, string colName) {
      if (!row.Table.Columns.Contains(colName)) { return null; }
      return row[colName].ToString();
    }
  }
}

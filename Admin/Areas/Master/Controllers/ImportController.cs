using Admin.Contract.Interface.Implementation;
using Dapper;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System.Data;
using System.Text;

namespace Admin.Web.Areas.Master.Controllers
{

    [Area("Master")]
    [Route("Master/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class ImportController : Controller
    {
        private readonly IUserRepositry _userDataService;
        private readonly IMasterDataService _masterDataService;
        

        public ImportController(IUserRepositry userDataService ,IMasterDataService masterDataService)
        {
            _userDataService = userDataService;
            _masterDataService = masterDataService;
        }
        public IActionResult Index()
        {
            return View();
        }

      
        public IActionResult UploadExcelData(string TblName, IFormFile file)
        { string msg = "";
            try
            {
                if (file != null && file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);

                        var excelData = ReadExcelData(stream);
                        msg = _masterDataService.ExcelUpload(TblName, excelData);

                    }
                    return Ok("Data imported successfully");
                }
                else
                {
                    return BadRequest("No file or empty file received");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
      
        
        
        public async Task<IActionResult> ExportDataToExcel(string sqlQuery)
        {
            var excelBytes = await _masterDataService.GetExcelFromQuery(sqlQuery);
           // return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
            return File(excelBytes, "application/vnd.ms-excel", "data.xls");
        }






        public List<Dictionary<string, object>> ReadExcelData(MemoryStream stream)
        {
            var excelData = new List<Dictionary<string, object>>();

            using (var document = SpreadsheetDocument.Open(stream, false))
            {
                WorkbookPart workbookPart = document.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.FirstOrDefault();

                if (worksheetPart != null)
                {
                    SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                    Row headerRow = sheetData.Elements<Row>().First();

                    var columnHeaders = new List<string>();
                    foreach (Cell cell in headerRow.Elements<Cell>())
                    {
                        columnHeaders.Add(GetCellValue(cell, workbookPart));
                    }

                    foreach (Row row in sheetData.Elements<Row>().Skip(1))
                    {
                        var rowData = new Dictionary<string, object>();

                        int columnIndex = 0;
                        foreach (Cell cell in row.Elements<Cell>())
                        {
                            rowData[columnHeaders[columnIndex]] = GetCellValue(cell, workbookPart);
                            columnIndex++;
                        }

                        excelData.Add(rowData);
                    }
                }
            }

            return excelData;
        }


        private string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            string cellValue = string.Empty;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
                if (sharedStringPart != null)
                {
                    cellValue = sharedStringPart.SharedStringTable.ElementAt(int.Parse(cell.InnerText)).InnerText;
                }
            }
            else if (cell.CellValue != null)
            {
                cellValue = cell.CellValue.InnerText;

                // Check if the cell has a style index and format
                if (cell.StyleIndex != null)
                {
                    CellFormat cellFormat = (CellFormat)workbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ElementAt((int)cell.StyleIndex.Value);
                    if (cellFormat != null && cellFormat.NumberFormatId != null)
                    {
                        uint formatId = cellFormat.NumberFormatId.Value;

                        // Check for date/time formats (you may need to adjust the format IDs)
                        if (IsDateTimeFormat(formatId))
                        {
                            double doubleValue;
                            if (double.TryParse(cellValue, out doubleValue))
                            {
                                DateTime dateTime = DateTime.FromOADate(doubleValue);
                                cellValue = dateTime.ToString("yyyy-MM-dd HH:mm:ss"); // Adjust date/time format as needed
                            }
                        }
                    }
                }
            }

            return cellValue;
        }

       
        private bool IsDateTimeFormat(uint formatId)
        {
            return (formatId >= 14 && formatId <= 22) // Example format IDs for date/time (modify based on your Excel)
                || formatId == 165 // Additional format ID for a specific date/time format
                                   // Add more format IDs as needed for your specific date/time formats
                ;
        }

        
      

    }
}

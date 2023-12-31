
using System.Data;

namespace Admin.Contract.Interface.Implementation
{
    public interface IMasterDataService
    {
        //void BulkInsertData(IDbConnection connection, string tableName, DataTable dataTable);
        public string ExcelUpload(string TblName, object excelData);
        public Task<byte[]> GetExcelFromQuery(string sqlQuery);
        public  Task<string> CVSDownload(string SQLQuary);
    }
}

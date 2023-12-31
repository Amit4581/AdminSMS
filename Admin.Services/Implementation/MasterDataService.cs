using Admin.Contract.DBObjects;
using Admin.Contract.Interface.Implementation;
using Admin.Contract.Interface.Infrastructure;
using Admin.Contract.Models.User;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Reflection.Metadata;
using System.Text;

namespace Admin.Services.Repository.Implementation
{
    public class MasterDataService : IMasterDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConnectionFactory _connectionFactory;
        public MasterDataService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        public string ExcelUpload(string TblName, object excelData)
        {
            string msg = "";
            try
            {
                using (var connection = _connectionFactory.GetConnection)
                {
                    connection.Open();
                    var dataTable = ConvertToDataTable(excelData as List<Dictionary<string, object>>);
                    string columns = string.Join(", ", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                    string parameters = string.Join(", ", dataTable.Columns.Cast<DataColumn>().Select(c => "@" + c.ColumnName));
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string sql = $"INSERT INTO {TblName} ({columns}) VALUES ({parameters})";

                        // Create a dynamic parameter object to map the DataRow values to SQL parameters
                        var dynamicParameters = new DynamicParameters();
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            dynamicParameters.Add("@" + column.ColumnName, row[column.ColumnName]);
                        }

                        connection.Execute(sql, dynamicParameters);
                    }

                }
                msg = "Data imported successfully";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                // Handle exception (log, return error message, etc.)
                throw;
            }
            return msg;
        }

        public void BulkInsertData(IDbConnection connection, string tableName, DataTable dataTable)
        {
            string columns = string.Join(", ", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
            string parameters = string.Join(", ", dataTable.Columns.Cast<DataColumn>().Select(c => "@" + c.ColumnName));

            string sql = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

            connection.Execute(sql, dataTable);
        }

        public DataTable ConvertToDataTable(List<Dictionary<string, object>> excelData)
        {
            var dataTable = new DataTable();
            if (excelData?.Count > 0)
            {
                foreach (var column in excelData[0].Keys)
                {
                    dataTable.Columns.Add(column);
                }

                foreach (var row in excelData)
                {
                    var dataRow = dataTable.NewRow();
                    foreach (var column in row.Keys)
                    {
                        dataRow[column] = row[column];
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }


        public async Task<byte[]> GetExcelFromQuery(string sqlQuery)
        {
            var dataTable = new DataTable();

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;

                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            return ConvertDataTableToExcel(dataTable);
        }
        private byte[] ConvertDataTableToExcel(DataTable dataTable)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                // Write the Excel file content in XML format
                streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                streamWriter.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
                streamWriter.WriteLine(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
                streamWriter.WriteLine(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
                streamWriter.WriteLine(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
                streamWriter.WriteLine(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">");

                streamWriter.WriteLine("<Worksheet ss:Name=\"Sheet1\">");
                streamWriter.WriteLine("<Table>");

                // Write the header row
                streamWriter.WriteLine("<Row>");
                foreach (DataColumn column in dataTable.Columns)
                {
                    streamWriter.WriteLine($"<Cell><Data ss:Type=\"String\">{EscapeXml(column.ColumnName)}</Data></Cell>");
                }
                streamWriter.WriteLine("</Row>");

                // Write data rows
                foreach (DataRow row in dataTable.Rows)
                {
                    streamWriter.WriteLine("<Row>");
                    foreach (var value in row.ItemArray)
                    {
                        streamWriter.WriteLine($"<Cell><Data ss:Type=\"String\">{EscapeXml(value.ToString())}</Data></Cell>");
                    }
                    streamWriter.WriteLine("</Row>");
                }

                streamWriter.WriteLine("</Table>");
                streamWriter.WriteLine("</Worksheet>");
                streamWriter.WriteLine("</Workbook>");

                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }

        private string EscapeXml(string input)
        {
            return input.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }
       
        public async Task<string> CVSDownload(string sqlQuery)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = await connection.QueryAsync(sqlQuery); // Execute the dynamic SQL query using Dapper

                return DataTableToCsv(result);
            }
        }



        private string DataTableToCsv<T>(IEnumerable<T> data)
        {
            var dataTable = new DataTable();
            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach (var item in data)
            {
                var values = props.Select(prop => prop.GetValue(item) ?? "").ToArray();
                dataTable.Rows.Add(values);
            }

            var csv = new StringBuilder();

            foreach (DataColumn column in dataTable.Columns)
            {
                csv.Append(EscapeCsvField(column.ColumnName));
                csv.Append(',');
            }

            csv.AppendLine();

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var value in row.ItemArray)
                {
                    csv.Append(EscapeCsvField(value.ToString()));
                    csv.Append(',');
                }
                csv.AppendLine();
            }

            return csv.ToString();
        }

        private string EscapeCsvField(string field)
        {
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                // If the field contains a comma, double quote, or newline, escape it with double quotes
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return field;
        }



    }
}



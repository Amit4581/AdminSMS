using Admin.Contract.Interface.Implementation;
using Admin.Contract.Interface.Infrastructure;
using Admin.Contract.Model.Master;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Text.Json;

namespace Admin.Web.Areas.Master.Controllers
{
    [Area("Master")]
    [Route("Master/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class FormMasterController : Controller
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IUserRepositry _userDataService;
        public FormMasterController(IUserRepositry userDataService, IConnectionFactory connectionFactory)
        {
            _userDataService = userDataService;
            _connectionFactory = connectionFactory;
        }

        public async Task<IActionResult> Index()
        {
            var model = new List<TableMasterModel>();
            using (var conn = _connectionFactory.GetConnection)
            {
                conn.Open();
                var query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME ASC ";
                model = (await conn.QueryAsync<TableMasterModel>(query, null)).ToList();
            }
            return View(model);
        }
        public IActionResult MyForm(string TblName)
        {
            ViewBag.TblName = TblName;
            // Replace with your actual table name
            var columns = GetTableColumns(TblName);
            return View(columns);
        }

        public IActionResult SaveData1([FromBody] JsonElement formData, string TblName)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                conn.Open();
                // Build the SQL query to insert data into the CITYMAST table
                //var columns = formData.EnumerateObject().Select(p => p.Name);

                var columns = formData.EnumerateObject()
                          .Where(p => p.Value.ValueKind != JsonValueKind.Null && !p.Value.ToString().Equals(""))
                          .Select(p => p.Name);

                var values = string.Join(", ", columns.Select(name => "@" + name));
                var query = $"INSERT INTO {TblName} ({string.Join(", ", columns)}) VALUES ({values})";

                // Create a dynamic parameter object with parameter names matching the column names
                var parameters = new DynamicParameters();
                foreach (var property in formData.EnumerateObject().Where(p => p.Value.ValueKind != JsonValueKind.Null && !p.Value.ToString().Equals("")))
                {
                    // Convert JsonElement values to appropriate types
                    object value;
                    switch (property.Value.ValueKind)
                    {
                        case JsonValueKind.String:
                            if (property.Name.ToLower().Contains("date"))
                            {
                                // Specify the expected date format for parsing
                                string[] formats = { "yyyy-MM-dd", "M/d/yyyy", "MM/dd/yyyy", "yyyy-MM-ddTHH:mm:ss.fff" }; // Add your specific formats here
                                if (DateTime.TryParseExact(property.Value.GetString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                                {
                                    value = parsedDate;
                                }
                                else
                                {

                                    throw new FormatException($"Invalid date format for {property.Name}");
                                }
                            }
                            else
                            {
                                value = property.Value.GetString();
                            }
                            break;                          

                        case JsonValueKind.Number:
                            value = property.Value.GetInt32(); // Adjust this based on the actual type of the column
                            break;
                        case JsonValueKind.True:
                        case JsonValueKind.False:
                            value = property.Value.GetBoolean();
                            break;
                        case JsonValueKind.Null:
                            value = DBNull.Value;
                            break;
                        default:
                            throw new NotSupportedException($"Unsupported JSON value kind: {property.Value.ValueKind}");
                    }

                    parameters.Add("@" + property.Name, value);
                }

                // Execute the query with the dynamic parameters
                conn.Execute(query, parameters);
            }

            return Ok(new { success = true });
        }


        public IActionResult SaveData([FromBody] JsonElement formData, string TblName)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                conn.Open();
                // Build the SQL query to insert data into the CITYMAST table
                //var columns = formData.EnumerateObject().Select(p => p.Name);

                var columns = formData.EnumerateObject()
                          .Where(p => p.Value.ValueKind != JsonValueKind.Null && !p.Value.ToString().Equals(""))
                          .Select(p => p.Name);

                var values = string.Join(", ", columns.Select(name => "@" + name));
                var query = $"INSERT INTO {TblName} ({string.Join(", ", columns)}) VALUES ({values})";

                // Create a dynamic parameter object with parameter names matching the column names
                var parameters = new DynamicParameters();
                foreach (var columnName in columns)
                {
                    // Convert JsonElement values to appropriate types
                    object value;
                    var property = formData.GetProperty(columnName);
                    switch (property.ValueKind)
                    {
                        case JsonValueKind.String when columnName.ToLower().Contains("date"):
                            string[] formats = { "yyyy-MM-dd", "M/d/yyyy", "MM/dd/yyyy", "yyyy-MM-ddTHH:mm:ss.fff" };
                            if (DateTime.TryParseExact(property.GetString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                            {
                                value = parsedDate;
                            }
                            else
                            {
                                throw new FormatException($"Invalid date format for {columnName}");
                            }
                            break;

                        case JsonValueKind.String:
                            value = property.GetString();
                            break;

                        case JsonValueKind.Number:
                            value = property.GetInt32(); // Adjust this based on the actual type of the column
                            break;
                        case JsonValueKind.True:
                        case JsonValueKind.False:
                            value = property.GetBoolean();
                            break;
                        case JsonValueKind.Null:
                            value = DBNull.Value;
                            break;
                        default:
                            throw new NotSupportedException($"Unsupported JSON value kind: {property.ValueKind}");
                    }

                    parameters.Add("@" + columnName, value);
                }

                // Execute the query with the dynamic parameters
                conn.Execute(query, parameters);
            }

            return Ok(new { success = true });
        }
        private List<FormMasterModel> GetTableColumns(string tableName)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                string query = "SELECT COLUMN_NAME AS ColumnName, DATA_TYPE AS DataType,CHARACTER_MAXIMUM_LENGTH ,IS_NULLABLE as IS_NULLABLE,COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') IsIdentity FROM INFORMATION_SCHEMA.COLUMNS WHERE  TABLE_NAME = @TableName";
                conn.Open();
                var columns = conn.Query<FormMasterModel>(
                  query, new { TableName = tableName }
                ).ToList();


                //var columns = conn.Query<FormMasterModel>(
                //    "SELECT COLUMN_NAME AS ColumnName, DATA_TYPE AS DataType,CHARACTER_MAXIMUM_LENGTH ,IS_NULLABLE as IS_NULLABLE " +
                //    "FROM INFORMATION_SCHEMA.COLUMNS " +
                //    "WHERE  COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 0 AND TABLE_NAME = @TableName",
                //    new { TableName = tableName }
                //).ToList();

                return columns;
            }
        }


        [HttpPost]
        public IActionResult GetTabledata(string tableName)
        {
            var data = GetAllData(tableName);
            return Json(data);
        }


        private IEnumerable<dynamic> GetAllData(string tableName)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                conn.Open();
                var sql = $"SELECT * FROM {tableName}";
                var data = conn.Query(sql);
                return data;
            }
        }


        public IActionResult UpdateData([FromBody] JsonElement formData, string TblName)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                var columnsContainingId = formData.EnumerateObject()
                    .Where(p => p.Name.ToLower().Contains("id"))
                    .Select(p => p.Name);

                // Build the SQL query to update data in the specified table based on certain conditions
                var columnsToUpdate = formData.EnumerateObject().Where(p => !columnsContainingId.Contains(p.Name)).Select(p => $"{p.Name} = @{p.Name}");
                var whereConditions = columnsContainingId.Select(name => $"{name} = @{name}").ToList();
                var query = $"UPDATE {TblName} SET {string.Join(", ", columnsToUpdate)}  WHERE {string.Join(" AND ", whereConditions)}";

                // Create a dynamic parameter object with parameter names matching the column names
                var parameters = new DynamicParameters();

                foreach (var property in formData.EnumerateObject())
                {
                    // Convert JsonElement values to appropriate types
                    object value;
                    switch (property.Value.ValueKind)
                    {
                        case JsonValueKind.String:
                            if (property.Name.ToLower().Contains("date"))
                            {
                                // Specify the expected date format for parsing
                                string[] formats = { "yyyy-MM-dd", "M/d/yyyy", "MM/dd/yyyy","yyyy-MM-ddTHH:mm:ss.fff" }; // Add your specific formats here
                                if (DateTime.TryParseExact(property.Value.GetString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                                {
                                    value = parsedDate;
                                }
                                else
                                {

                                    throw new FormatException($"Invalid date format for {property.Name}");
                                }
                            }
                            else
                            {
                                value = property.Value.GetString();
                            }
                            break;
                        case JsonValueKind.Number:
                            value = property.Value.GetInt32(); // Adjust this based on the actual type of the column
                            break;
                        case JsonValueKind.True:
                        case JsonValueKind.False:
                            value = property.Value.GetBoolean();
                            break;
                        case JsonValueKind.Null:
                            value = DBNull.Value;
                            break;
                        default:
                            throw new NotSupportedException($"Unsupported JSON value kind: {property.Value.ValueKind}");
                    }

                    parameters.Add("@" + property.Name, value);
                }


                // Add parameters for columns containing 'id' in their names
                foreach (var columnName in columnsContainingId)
                {
                    parameters.Add("@" + columnName, formData.GetProperty(columnName).GetString());
                }
                conn.Open();
                // Execute the query with the dynamic parameters
                conn.Execute(query, parameters);
            }
            return Json("");
        }

        public IActionResult Deletedata([FromBody] JsonElement formData, string TblName)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                var columnsContainingId = formData.EnumerateObject()
                    .Where(p => p.Name.ToLower().Contains("id"))
                    .Select(p => p.Name);

                // Build the SQL query to update data in the specified table based on certain conditions
                // var columnsToUpdate = formData.EnumerateObject().Select(p => $"{p.Name} = @{p.Name}");
                var whereConditions = columnsContainingId.Select(name => $"{name} = @{name}").ToList();
                var query = $"Delete from {TblName}  WHERE {string.Join(" AND ", whereConditions)}";

                // Create a dynamic parameter object with parameter names matching the column names
                var parameters = new DynamicParameters();

                // Add parameters for columns containing 'id' in their names
                foreach (var columnName in columnsContainingId)
                {
                    var value = formData.GetProperty(columnName).GetString();
                    parameters.Add("@" + columnName, formData.GetProperty(columnName).GetString());
                }
                conn.Open();
                // Execute the query with the dynamic parameters
                conn.Execute(query, parameters);
            }
            return Json("");
        }


    }
}

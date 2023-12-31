using Admin.Common.HelperClass;
using Admin.Contract.DBObjects;
using Admin.Contract.Interface.Implementation;
using Admin.Contract.Model.Master;
using Admin.Contract.Model.Student;
using Admin.Contract.Models.HttpRequestResponse;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Net;

namespace Admin.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUserRepositry _userDataService;

        public StudentController(IUserRepositry userDataService)
        {
            _userDataService = userDataService;
        }
        public async Task<IActionResult> StudentsList()
        {
            var query = SqlQry.StudentList;
            IEnumerable<StudentDetails> result = await _userDataService.ExcuteQuaryAsync<StudentDetails>(query, null);
            return View(result);
        }
        [HttpGet]
        public async Task<JsonResult> StudentList()
        {
            try
            {
                var query = SqlQry.StudentList;
                IEnumerable<StudentDetails> result = await _userDataService.ExcuteQuaryAsync<StudentDetails>(query, null);
                return Json(new ResponseDataEnvelop<IEnumerable<StudentDetails>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Json(new ReponseMessageEnvelop
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddStudent()
        {
            //StudentList
            //StudentDetails
            var query = StoredProcedures.GETTYPEMASTER;
            var parameters = new DynamicParameters();
            parameters.Add("@Type", "Gender");
            IEnumerable<TypeMaster> typemaster = await _userDataService.ExcuteSPAsync<TypeMaster>(query, parameters);
            var GendertList = SelectListHelper.ConvertToSelectList(typemaster, m => m.Type_Code.ToString(), m => m.Type_Name);
            ViewBag.Gender = GendertList;

            var query1 = SqlQry.GetBloodGroopDD;
            IEnumerable<TypeMaster> result = await _userDataService.ExcuteQuaryAsync<TypeMaster>(query1, null);
            var BloodGroup = SelectListHelper.ConvertToSelectList(result, m => m.Type_Code.ToString(), m => m.Type_Name);
            ViewBag.BloodGroup = BloodGroup;

            var query2 = SqlQry.GetClassDD;
            IEnumerable<TypeMaster> result1 = await _userDataService.ExcuteQuaryAsync<TypeMaster>(query2, null);
            var classes = SelectListHelper.ConvertToSelectList(result1, m => m.Type_Code.ToString(), m => m.Type_Name);
            ViewBag.classes = classes;

            var query3 = SqlQry.GetSectionDD;
            IEnumerable<TypeMaster> result3 = await _userDataService.ExcuteQuaryAsync<TypeMaster>(query3, null);
            var Section = SelectListHelper.ConvertToSelectList(result3, m => m.Type_Code.ToString(), m => m.Type_Name);
            ViewBag.Section = Section;


            var query4 = StoredProcedures.GETCITYDD;
            var parameters3 = new DynamicParameters();
            parameters3.Add("@Type", "State");
            IEnumerable<TypeMaster> typemaster3 = await _userDataService.ExcuteSPAsync<TypeMaster>(query4, parameters3);
            var StateList = SelectListHelper.ConvertToSelectList(typemaster3, m => m.Type_Code.ToString(), m => m.Type_Name);
            ViewBag.State = StateList;

            var parameters4 = new DynamicParameters();
            parameters4.Add("@Type", "Country");
            IEnumerable<TypeMaster> typemaster4 = await _userDataService.ExcuteSPAsync<TypeMaster>(query4, parameters4);
            var CountryList = SelectListHelper.ConvertToSelectList(typemaster4, m => m.Type_Code.ToString(), m => m.Type_Name);
            ViewBag.Country = CountryList;


            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddStudentDetails([FromForm] StudentDetails std)
        {
            string msg = "";
            if (!ModelState.IsValid)
            {
                try
                {
                    var query = StoredProcedures.INSERTUPDATESTUDENTDETAILS;
                    var parameters = new DynamicParameters();
                    parameters.Add("@StudentId", std.StudentId);
                    parameters.Add("@StudentName", std.StudentName);
                    parameters.Add("@DOB", std.DOB);
                    parameters.Add("@Gender", std.Gender);
                    parameters.Add("@StudentEmail", std.StudentEmail);
                    parameters.Add("@StudentPhone", std.StudentPhone);
                    parameters.Add("@Address1", std.Address1);
                    parameters.Add("@Address2", std.Address2);
                    parameters.Add("@City", std.City);
                    parameters.Add("@State", std.State);
                    parameters.Add("@Country", std.Country);
                    parameters.Add("@PostalCode", std.PostalCode);
                    parameters.Add("@FathersName", std.FathersName);
                    parameters.Add("@MothersName", std.MothersName);
                    parameters.Add("@GurdianName", std.GurdianName);
                    parameters.Add("@EmergencyContact", std.EmergencyContact);
                    parameters.Add("@Natinality", std.Natinality);
                    parameters.Add("@StudentBloodGroup", std.StudentBloodGroup);
                    parameters.Add("@MedicalHistory", std.MedicalHistory);
                    parameters.Add("@ClassId", std.ClassId);
                    parameters.Add("@SectionId", std.SectionId);
                    parameters.Add("@FileInput", std.FileInput);
                    

                    var result = await _userDataService.ExcuteSPAsync<StudentDetails>(query, parameters);
                    msg = "Success";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                return Json(msg);
            }
            return Json(msg);
        }

        [HttpPost]
        public async Task<JsonResult> GetStudentDetails(string StudentId)
        {
            var query = StoredProcedures.GETSTUDENTDD;
            var parameters = new DynamicParameters();
            parameters.Add("@Student_Code", StudentId);
            IEnumerable<TypeMaster> typemaster = await _userDataService.ExcuteSPAsync<TypeMaster>(query, parameters);
            var EmpList = SelectListHelper.ConvertToSelectList(typemaster, m => m.Type_Code.ToString(), m => m.Type_Name);
            return Json(EmpList);
        }


        [HttpPost]
        public async Task<JsonResult> GetEmployeeDetails(string StudentId)
        {
            var query = StoredProcedures.GETSTUDENTDETAILS;
            var parameters = new DynamicParameters();
            parameters.Add("@StudentId", StudentId);
            var employees = await _userDataService.SingleModelSPAsync<StudentDetails>(query, parameters);
            return Json(new ResponseDataEnvelop<StudentDetails>
            {
                StatusCode = HttpStatusCode.OK,
                Data = employees
            });
        }

        //[HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile()
        {
            var file = Request.Form.Files[0]; // Assuming only one file is uploaded

            if (file == null || file.Length == 0)
            {
                return BadRequest("File not selected or invalid.");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath });
        }
    }
}

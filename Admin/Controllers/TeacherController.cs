using Admin.Common.HelperClass;
using Admin.Contract.DBObjects;
using Admin.Contract.Interface.Implementation;
using Admin.Contract.Model.Master;
using Admin.Contract.Model.Student;
using Admin.Contract.Model.Teacher;
using Admin.Contract.Models.HttpRequestResponse;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Admin.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IUserRepositry _userDataService;

        public TeacherController(IUserRepositry userDataService)
        {
            _userDataService = userDataService;
        }
        public async Task<IActionResult> TeacherList()
        {

            var query = SqlQry.GetTeacherDetails;
            IEnumerable<TeacherDetails> result = await _userDataService.ExcuteQuaryAsync<TeacherDetails>(query, null);
            return View(result);

        }
        [HttpGet]
        public async Task<IActionResult> Addteacher()
        {
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
        public async Task<JsonResult> AddteacherDetails([FromForm] TeacherDetails td)
        {
            string msg = "";
            if (!ModelState.IsValid)
            {
                try
                {
                    var query = StoredProcedures.INSERTUPDATE_TEACHERDETAILS;
                    var parameters = new DynamicParameters();
                    parameters.Add("@TeacherId", td.TeacherId);
                    parameters.Add("@TeacherFName", td.TeacherFName);
                    parameters.Add("@TeacherLName", td.TeacherLName);
                    parameters.Add("@DOB", td.DOB);
                    parameters.Add("@JoiningDate", td.JoiningDate);
                    parameters.Add("@Gender", td.Gender);
                    parameters.Add("@SubjectTaught", td.SubjectTaught);
                    parameters.Add("@Department", td.Department);
                    parameters.Add("@Qualification", td.Qualification);
                    parameters.Add("@Salary", td.Salary);
                    parameters.Add("@TeacherEmail", td.TeacherEmail);
                    parameters.Add("@TeacherPhone", td.TeacherPhone);
                    parameters.Add("@Address1", td.Address1);
                    parameters.Add("@Address2", td.Address2);
                    parameters.Add("@City", td.City);
                    parameters.Add("@State", td.State);
                    parameters.Add("@Country", td.Country);
                    parameters.Add("@PostalCode", td.PostalCode);
                    parameters.Add("@FathersName", td.FathersName);
                    parameters.Add("@MothersName", td.MothersName);
                    parameters.Add("@EmergencyContact", td.EmergencyContact);
                    parameters.Add("@Natinality", "1");
                    parameters.Add("@TeacherBloodGroup", td.TeacherBloodGroup);
                    parameters.Add("@MedicalHistory", td.MedicalHistory);
                    parameters.Add("@Imageupload", td.Imageupload);


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
        public async Task<JsonResult> GetTeacherName(string Teacher_Code)
        {
            var query = StoredProcedures.GETTEACHERDD;
            var parameters = new DynamicParameters();
            parameters.Add("@Teacher_Code", Teacher_Code);
            IEnumerable<TypeMaster> typemaster = await _userDataService.ExcuteSPAsync<TypeMaster>(query, parameters);
            var EmpList = SelectListHelper.ConvertToSelectList(typemaster, m => m.Type_Code.ToString(), m => m.Type_Name);
            return Json(EmpList);
        }


        [HttpPost]
        public async Task<JsonResult> GetTeachdetails(string TeacherId)
        {
            var query = StoredProcedures.GET_TEACHER_DETAILS;
            var parameters = new DynamicParameters();
            parameters.Add("@TeacherId", TeacherId);
            var employees = await _userDataService.SingleModelSPAsync<TeacherDetails>(query, parameters);
            return Json(new ResponseDataEnvelop<TeacherDetails>
            {
                StatusCode = HttpStatusCode.OK,
                Data = employees
            });
        }
    }
}

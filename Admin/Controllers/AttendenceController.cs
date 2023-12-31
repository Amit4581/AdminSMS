using Admin.Common.HelperClass;
using Admin.Contract.DBObjects;
using Admin.Contract.Interface.Implementation;
using Admin.Contract.Model.Attendence;
using Admin.Contract.Model.Master;
using Admin.Contract.Model.Student;
using Admin.Contract.Models.HttpRequestResponse;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Admin.Web.Controllers
{
    public class AttendenceController : Controller
    {
        private readonly IUserRepositry _userDataService;

        public AttendenceController(IUserRepositry userDataService)
        {
            _userDataService = userDataService;
        }
        public async Task<IActionResult> Index()
        {
            var query = StoredProcedures.ManageStudentAttendenceLog;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "ALL");
            IEnumerable<StudentAttendenceLog> result = await _userDataService.ExcuteQuaryAsync<StudentAttendenceLog>(query, parameters);
            return View(result);
        }
        public async Task<IActionResult> GetAllAttendence()
        {
            var query = StoredProcedures.ManageStudentAttendenceLog;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "ALL");
            var results = await _userDataService.ExcuteQuaryAsync<StudentAttendenceLog>(query, parameters);

            var events = new List<object>();
            foreach (var result in results)
            {
                events.Add(new
                {
                    id = result.Id, // Assuming Id is the property for the id
                    start = result.AtDate,
                    end = result.AtDate,
                    title = result.StudentName,
                    backgroundColor = "#f5b849",
                    //borderColor = result.BorderColor,
                    borderColor = "#f5b849",
                    description = result.LeaveType,
                });
            }

            return Json(events);
        }

        public async Task<IActionResult> AddAttendence()
        {
            var query2 = SqlQry.GetClassDD;
            IEnumerable<TypeMaster> result1 = await _userDataService.ExcuteQuaryAsync<TypeMaster>(query2, null);
            var classes = SelectListHelper.ConvertToSelectList(result1, m => m.Type_Code.ToString(), m => m.Type_Name);
            ViewBag.classes = classes;

            var query3 = SqlQry.GetSectionDD;
            IEnumerable<TypeMaster> result3 = await _userDataService.ExcuteQuaryAsync<TypeMaster>(query3, null);
            var Section = SelectListHelper.ConvertToSelectList(result3, m => m.Type_Code.ToString(), m => m.Type_Name);
            ViewBag.Section = Section;


            return View();
        }
        [HttpGet]
        public async Task<JsonResult> StudentList( string ClassId, string SectionId)
        {
            string query = "";
            try
            {
                if (SectionId == null || SectionId == "") {
                    query = "select StudentId,StudentName,s.ClassId,cm.ClassName,s.SectionId,sm.SectionNames from StudentDetails s inner join ClassMast cm on s.ClassId= cm.ClassId inner join SectionMast sm on s.SectionId = sm.SectionId where cm.ClassId='" + ClassId + "'  and s.IsActive='1'";
                }
                else
                {
                    query = "select StudentId,StudentName,s.ClassId,cm.ClassName,s.SectionId,sm.SectionName from StudentDetails s inner join ClassMast cm on s.ClassId= cm.ClassId inner join SectionMast sm on s.SectionId = sm.SectionId where cm.ClassId='" + ClassId + "' and sm.SectionId='" + SectionId + "' and s.IsActive='1'";
                }
                // query = SqlQry.StudentList;
                IEnumerable<StudentAttendenceLog> result = await _userDataService.ExcuteQuaryAsync<StudentAttendenceLog>(query, null);
                return Json(new ResponseDataEnvelop<IEnumerable<StudentAttendenceLog>>
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
    }
}

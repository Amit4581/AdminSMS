using Admin.Contract.DBObjects;
using Admin.Contract.Interface.Implementation;
using Admin.Contract.Model.Exam;
using Admin.Contract.Model.Master;
using Admin.Contract.Models.HttpRequestResponse;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Admin.Web.Areas.Exam.Controllers
{
    [Area("Exam")]
    [Route("Exam/[controller]/[action]")]

    public class MCQStudentResultController : Controller
    {
        private readonly IUserRepositry _userDataService;

        public MCQStudentResultController(IUserRepositry userDataService)
        {
            _userDataService = userDataService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var query = StoredProcedures.ManageStudentMCQResult;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "ALL");
            IEnumerable<MCQStudentResultSheet> result = await _userDataService.ExcuteSPAsync<MCQStudentResultSheet>(query, parameters);
            return Json(new ResponseDataEnvelop<IEnumerable<MCQStudentResultSheet>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = result
            });
        }

        public async Task<IActionResult> GetSingle(string Id)
        {
            var query = StoredProcedures.ManageStudentMCQResult;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "SELECT");
            parameters.Add("@Id", Id);
            var result = await _userDataService.SingleModelSPAsync<MCQStudentResultSheet>(query, parameters);
            return Json(new ResponseDataEnvelop<MCQStudentResultSheet>
            {
                StatusCode = HttpStatusCode.OK,
                Data = result
            });
        }


        [HttpPost]
        public async Task<JsonResult> AddSYS([FromForm] MCQStudentResultSheet m)
        {
            string msg = "";
            if (!ModelState.IsValid)
            {
                try
                {
                    var query = StoredProcedures.ManageStudentMCQResult;
                    var parameters = new DynamicParameters();
                    parameters.Add("@COMP_CODE", m.COMP_CODE);
                    parameters.Add("@StudentId", m.StudentId);
                    parameters.Add("@CourseId", m.CourseId);
                    parameters.Add("@CourseName", m.CourseName);
                    parameters.Add("@ClassId", m.ClassId);
                    parameters.Add("@QsId", m.QsId);
                    parameters.Add("@QsNo", m.QsNo);
                    parameters.Add("@Qs", m.Qs);
                    parameters.Add("@AnsGiven", m.AnsGiven);
                    parameters.Add("@CorrectAns", m.CorrectAns);
                    parameters.Add("@MarkObtain", m.MarkObtain);
                    parameters.Add("@Action", m.Action);
                    var result = await _userDataService.ExcuteSPAsync<MCQStudentResultSheet>(query, parameters);
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
        public async Task<JsonResult> Delet(string Id)
        {
            string msg = "";
            try
            {
                var query = StoredProcedures.ManageStudentMCQResult;
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id);
                parameters.Add("@Action", "DELETE");
                var result = await _userDataService.ExcuteSPAsync<MCQStudentResultSheet>(query, parameters);
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(msg);

        }

    }
}

using Admin.Contract.Interface.Implementation;
using Microsoft.AspNetCore.Mvc;
using Admin.Contract.Model.Exam;
using Admin.Contract.DBObjects;
using Admin.Contract.Model.Master;
using Admin.Contract.Models.HttpRequestResponse;
using Dapper;
using System.Net;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Admin.Web.Areas.Exam.Controllers
{
    [Area("Exam")]
    [Route("Exam/[controller]/[action]")]
    public class MCQController : Controller
    {
        private readonly IUserRepositry _userDataService;

        public MCQController(IUserRepositry userDataService)
        {
            _userDataService = userDataService;
        }
        public async Task<IActionResult> Index(string Id ,string ClassId)
        {
            ViewBag.CourseId = Id;
            ViewBag.ClassId = ClassId;
            var query = StoredProcedures.ManageMCQQS;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "SELECTCOURSE");
            parameters.Add("@CourseId", Id);
            var result = await _userDataService.ExcuteSPAsync<MCQQS>(query, parameters);
            return View(result);
        }
        public async Task<IActionResult> GetAllMCQ()
        {
            var query = StoredProcedures.ManageMCQQS;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "ALL");
            IEnumerable<MCQQS> result = await _userDataService.ExcuteSPAsync<MCQQS>(query, parameters);

            return Json(new ResponseDataEnvelop<IEnumerable<MCQQS>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = result
            });
        }

        public async Task<IActionResult> GetSingleMCQ(string Id)
        {

            var query = StoredProcedures.ManageMCQQS;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "SELECT");
            parameters.Add("@Id", Id);
            var result = await _userDataService.SingleModelSPAsync<MCQQS>(query, parameters);
            return Json(new ResponseDataEnvelop<MCQQS>
            {
                StatusCode = HttpStatusCode.OK,
                Data = result
            });
        }


        [HttpPost]
        public async Task<JsonResult> Add([FromForm] MCQQS m)
        {
            string msg = "";
            if (!ModelState.IsValid)
            {
                try
                {
                    var query = StoredProcedures.ManageMCQQS;
                    var parameters = new DynamicParameters();

                    parameters.Add("@COMP_CODE", m.Comp_Code);
                    parameters.Add("@CourseId", m.CourseId);
                    parameters.Add("@ClassId", m.ClassId);
                    parameters.Add("@QsId", m.QsId);
                    parameters.Add("@QsNo", m.QsNo);
                    parameters.Add("@Qs", m.Qs);
                    parameters.Add("@Op1", m.Op1);
                    parameters.Add("@Op2", m.Op2);
                    parameters.Add("@Op3", m.Op3);
                    parameters.Add("@Op4", m.Op4);
                    parameters.Add("@Ans", m.Ans);
                    parameters.Add("@Tag", m.Tag);
                    parameters.Add("@Action", m.Action);
                    var result = await _userDataService.ExcuteSPAsync<MCQQS>(query, parameters);
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
        public async Task<JsonResult> Delete(string Id)
        {
            string msg = "";
            try
            {
                var query = StoredProcedures.ManageMCQQS;
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id);
                parameters.Add("@Action", "DELETE");
                var result = await _userDataService.ExcuteSPAsync<MCQQS>(query, parameters);
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(msg);

        }


        [HttpPost]
        public async Task<JsonResult> SubmitAns(string id, string answerresult,string CourseID,string ClassId)
        {
            string userId = Convert.ToString(Request.Cookies["User_Code"]);
            string COMP_CODE = Convert.ToString(Request.Cookies["COMP_CODE"]);
            //
            string msg = "" +id+answerresult;

            var query = StoredProcedures.SubmitStudentMCQQuestion;
            var parameters = new DynamicParameters();
            parameters.Add("@QsId", id);
            parameters.Add("@COMP_CODE", COMP_CODE);
            parameters.Add("@StudentId", userId);
            parameters.Add("@CourseId", CourseID);
            parameters.Add("@ClassId", ClassId);
            parameters.Add("@AnsGiven", answerresult);
            parameters.Add("@Active", "1");
            parameters.Add("@Action", "INSERT");
            var result = await _userDataService.ExcuteSPAsync<MCQQS>(query, parameters);
            msg = "Success";

            return Json(msg);

        }



    }
}

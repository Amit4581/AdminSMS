using Admin.Contract.DBObjects;
using Admin.Contract.Interface.Implementation;
using Admin.Contract.Model.Exam;
using Admin.Contract.Models.HttpRequestResponse;
using Dapper;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Admin.Web.Areas.Exam.Controllers
{
    [Area("Exam")]
    [Route("Exam/[controller]/[action]")]

    public class MCQCourseController : Controller
    {
        private readonly IUserRepositry _userDataService;

        public MCQCourseController(IUserRepositry userDataService)
        {
            _userDataService = userDataService;
        }
        public async Task<IActionResult> Index()
        {
            //ManageCourseMaster   //CourseDetail
            var query = StoredProcedures.ManageCourseMaster;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "ALL");
            IEnumerable<CourseDetail> result = await _userDataService.ExcuteSPAsync<CourseDetail>(query, parameters);

            ViewBag.CourseMaster = result;

            var query1 = StoredProcedures.ManageMCQCourseDetail;
            var parameters1 = new DynamicParameters();
            parameters1.Add("@Action", "ALL");
            IEnumerable<MCQCourseDetail> result1 = await _userDataService.ExcuteSPAsync<MCQCourseDetail>(query1, parameters1);

            return View(result1);
        }


        public async Task<IActionResult> Instructions(string Id, string ClassId)
        {
            string COMP_CODE = Convert.ToString(Request.Cookies["COMP_CODE"]);
            string qry = "SELECT  CourseName, CourseID,CourseDesc, ClassId, Start_time, Timeduration, TotalMark, PassMark, CourseToken, MaxNoStudent  FROM  MCQCourseDetail where COMP_CODE=@COMP_CODE and CourseID=@CourseID and ClassId=@ClassId and  Active='1'";

            var parameters = new
            {
                COMP_CODE= COMP_CODE,
                CourseID = Id,
                ClassId = ClassId
            };
            var model = await _userDataService.GetSingleModelRecordAsync<MCQCourseDetail>(qry, parameters);            
            return View(model);
        }


        public async Task<IActionResult> GetAll()
        {
            var query = StoredProcedures.ManageMCQCourseDetail;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "ALL");
            IEnumerable<MCQCourseDetail> result = await _userDataService.ExcuteSPAsync<MCQCourseDetail>(query, parameters);

            return Json(new ResponseDataEnvelop<IEnumerable<MCQCourseDetail>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = result
            });
        }

        public async Task<IActionResult> GetSingle(string Id)
        {
            var query = StoredProcedures.ManageMCQCourseDetail;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "SELECT");
            parameters.Add("@Id", Id);
            var result = await _userDataService.SingleModelSPAsync<MCQCourseDetail>(query, parameters);
            return Json(new ResponseDataEnvelop<MCQCourseDetail>
            {
                StatusCode = HttpStatusCode.OK,
                Data = result
            });
        }


        [HttpPost]
        public async Task<JsonResult> Add([FromForm] MCQCourseDetail m)
        {
            string msg = "";
            if (!ModelState.IsValid)
            {
                try
                {
                    var query = StoredProcedures.ManageMCQCourseDetail;
                    var parameters = new DynamicParameters();

                    parameters.Add("@COMP_CODE", m.COMP_CODE);
                    parameters.Add("@CourseID", m.CourseID);
                    parameters.Add("@CourseName", m.CourseName);
                    parameters.Add("@CourseDesc", m.CourseDesc);
                    parameters.Add("@ClassId", m.ClassId);
                    parameters.Add("@Start_time", m.Start_time);
                    parameters.Add("@@Timeduration", m.Timeduration);
                    parameters.Add("@TotalMark", m.TotalMark);
                    parameters.Add("@PassMark", m.PassMark);
                    parameters.Add("@CourseToken", m.CourseToken);
                    parameters.Add("@MaxNoStudent", m.MaxNoStudent);
                    parameters.Add("@Action", m.Action);
                    var result = await _userDataService.ExcuteSPAsync<MCQCourseDetail>(query, parameters);
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
                var result = await _userDataService.ExcuteSPAsync<MCQCourseDetail>(query, parameters);
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

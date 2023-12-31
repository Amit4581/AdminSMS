using Admin.Contract.Interface.Implementation;
using Microsoft.AspNetCore.Mvc;
using Admin.Contract.Model.Master;
using Admin.Common.HelperClass;
using Admin.Contract.DBObjects;
using Dapper;
using Admin.Contract.Model.Student;
using Admin.Contract.Models.HttpRequestResponse;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Admin.Web.Areas.Master.Controllers
{

    [Area("Master")]
    [Route("Master/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class SetupController : Controller
    {
        private readonly IUserRepositry _userDataService;

        public SetupController(IUserRepositry userDataService)
        {
            _userDataService = userDataService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllSYS()
        {
            var query = StoredProcedures.SYSMaster;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "ALL");
            IEnumerable<SYS> result = await _userDataService.ExcuteSPAsync<SYS>(query, parameters);

            return Json(new ResponseDataEnvelop<IEnumerable<SYS>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = result
            });
        }


        public async Task<IActionResult> GetSingleSYS(string COMP_CODE)
        {

            var query = StoredProcedures.SYSMaster;
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "SELECT");
            parameters.Add("@COMP_CODE", COMP_CODE);
            var result = await _userDataService.SingleModelSPAsync<SYS>(query, parameters);

            return Json(new ResponseDataEnvelop<SYS>
            {
                StatusCode = HttpStatusCode.OK,
                Data = result
            });
        }


        [HttpPost]
        public async Task<JsonResult> AddSYS([FromForm] SYS s)
        {
            string msg = "";
            if (!ModelState.IsValid)
            {
                try
                {
                    var query = StoredProcedures.SYSMaster;
                    var parameters = new DynamicParameters();

                    parameters.Add("@COMP_CODE", s.COMP_CODE);
                    parameters.Add("@COMP_NAME", s.COMP_NAME);
                    parameters.Add("@Short_Name", s.Short_Name);
                    parameters.Add("@DATABASENAME", s.DATABASENAME);
                    parameters.Add("@USERID", s.USERID);
                    parameters.Add("@USERPASSWORD", s.USERPASSWORD);
                    parameters.Add("@DSN", s.DSN);
                    parameters.Add("@CONNECTSTRING", s.CONNECTSTRING);
                    parameters.Add("@ISACTIVE", "1");
                    parameters.Add("@Action",s.Action);
                    var result = await _userDataService.ExcuteSPAsync<SYS>(query, parameters);
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
        public async Task<JsonResult> DeletSYS([FromForm] SYS s)
        {
            string msg = "";
            try
            {
                var query = StoredProcedures.SYSMaster;
                var parameters = new DynamicParameters();
                parameters.Add("@COMP_CODE", s.COMP_CODE);
                parameters.Add("@Action", "DELETE");
                var result = await _userDataService.ExcuteSPAsync<SYS>(query, parameters);
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

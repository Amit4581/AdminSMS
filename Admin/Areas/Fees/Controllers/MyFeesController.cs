using Admin.Common.HelperClass;
using Admin.Contract.DBObjects;
using Admin.Contract.Interface.Implementation;
using Admin.Contract.Model.Fees;
using Admin.Contract.Model.Master;
using Microsoft.AspNetCore.Mvc;


namespace Admin.Web.Areas.Fees.Controllers
{
    [Area("Fees")]
    [Route("Fees/[controller]/[action]")]
    public class MyFeesController : Controller
    {
        private readonly IUserRepositry _userDataService;
        public MyFeesController(IUserRepositry userDataService)
        {
            _userDataService = userDataService;
        }
        public async Task<IActionResult> MyFeesMaster()
        {
            var query2 = SqlQry.GetClassDD;
            IEnumerable<TypeMaster> result1 = await _userDataService.ExcuteQuaryAsync<TypeMaster>(query2, null);
            var classes = SelectListHelper.ConvertToSelectList(result1, m => m.Type_Code.ToString(), m => m.Type_Name);
            ViewBag.classes = classes;
            return View();
        }
        public async Task<IActionResult> GetFeesMaster( string ClassId, string Month , string Year)
        {
            string COMP_CODE = Convert.ToString(Request.Cookies["COMP_CODE"]);
            string qry = "SELECT f.Id, f.COMP_CODE, cm.ClassName,f.ClassId, Month, Year, Amount,FeesType, GST,f.EntryDate, f.IsActive FROM FeesMaster f left join ClassMast cm on f.ClassId=cm.ClassId where f.COMP_CODE='" + COMP_CODE + "' and f.IsActive='1'";

            if (!string.IsNullOrEmpty(ClassId))
            {
                qry += " and f.ClassId='" + ClassId + "'";
            }

            if (!string.IsNullOrEmpty(Month))
            {
                qry += " and f.Month='" + Month + "'";
            }

            if (!string.IsNullOrEmpty(Year))
            {
                qry += " and f.Year='" + Year + "'";
            }
            //if (ClassId != null || ClassId != "")
            // {
            //     qry += "and f.ClassId='" + ClassId + "'";
            // }
            // if (Month != null || Month != "")
            // {
            //     qry += "and f.Month='" + Month + "'";
            // }
            // if (Year != null || Year != "")
            // {
            //     qry += "and f.Year='" + Year + "'";
            // }
            var model = await _userDataService.ExcuteQuaryAsync<FeesMaster>(qry, null);
            return Json(model);
        }

        
    }
}

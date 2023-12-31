using Admin.Contract.DBObjects;
using Admin.Contract.Interface.Implementation;
using Admin.Contract.Model.Home;
using Admin.Contract.Models.User;
using Admin.Models;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepositry _userDataService;

        public HomeController(ILogger<HomeController> logger , IUserRepositry userDataService)
        {
            _logger = logger;
            _userDataService = userDataService;
        }

        public async Task<IActionResult> Index()
        {
            string COMP_CODE = Convert.ToString(Request.Cookies["COMP_CODE"]);

            string TotalStudent = await _userDataService.GetSingleValueAsync<string>("select Count(StudentId) from StudentDetails where IsActive = 1 and  COMP_CODE = @COMP_CODE", new { COMP_CODE = COMP_CODE });   
            ViewBag.TotalStudent = TotalStudent;           

            string TotalTeacher = await _userDataService.GetSingleValueAsync<string>("select Count(TeacherId) from TeacherDetails where IsActive = 1 and  COMP_CODE = @COMP_CODE", new { COMP_CODE = COMP_CODE });
            ViewBag.TotalTeacher = TotalTeacher;

            string TotalExam = await _userDataService.GetSingleValueAsync<string>("select Count(CourseID) from MCQCourseDetail where Active = 1 and  COMP_CODE = @COMP_CODE", new { COMP_CODE = COMP_CODE });
            ViewBag.TotalExam = TotalExam;

            return View();
        }


        public async Task<IActionResult> GetSideBar()
        {
            string roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (roleClaim == "Admin")
            {
                var query = StoredProcedures.GET_WEB_MODULE;
                var parameters0 = new DynamicParameters();
                parameters0.Add("@parent", "SuperParent");
                IEnumerable<WebModules> webModulesSuperParent = await _userDataService.ExcuteSPAsync<WebModules>(query, parameters0);

                var parameters = new DynamicParameters();
                parameters.Add("@parent", "Parent");
                IEnumerable<WebModules> webModulesParents = await _userDataService.ExcuteSPAsync<WebModules>(query, parameters);

                var parameters2 = new DynamicParameters();
                parameters2.Add("@parent", "Chield");
                IEnumerable<WebModules> webModulesChield = await _userDataService.ExcuteSPAsync<WebModules>(query, parameters2);
                // return  Json(new { webModulesParents, webModulesChield });
                ViewBag.webModulesParents = webModulesParents;
                ViewBag.webModulesChield = webModulesChield;
                ViewBag.webModulesSuperParent = webModulesSuperParent;
                var data = new
                {
                    webModulesParents = webModulesParents,
                    webModulesChield = webModulesChield,
                    webModulesSuperParent = webModulesSuperParent
                };
            }
            return PartialView("_SideBar");

        }





        public IActionResult Ecommerce()
        {
            return View();
        }
        public IActionResult Crypto()
        {
            return View();
        }

        public IActionResult Jobs()
        {
            return View();
        }
        public IActionResult NFT()
        {
            return View();
        }
        public IActionResult Sales()
        {
            return View();
        }
        public IActionResult Analytics()
        {
            return View();
        }
        public IActionResult Projects()
        {
            return View();
        }
        public IActionResult HRM()
        {
            return View();
        }
        public IActionResult Stocks()
        {
            return View();
        }
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult Personal()
        {
            return View();
        }
        



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
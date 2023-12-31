using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.DBObjects
{
    public class StoredProcedures
    {

        public const string GET_LOGIN_OTP = "SP_GetLoginOtp";
        public const string CITY_MASTER = "Sp_City_Master";
        public const string COUNTRY_MAST = "Sp_CountryMast";
        public const string REGISTRATION = "SP_Registration";
        public const string INSERT_SINGLE_COUNTRY = "SP_InsertSingleCountry";
        public const string INSERT_COUNTRY = "SP_InsertCountry";
        public const string REGISTRION = "SP_Registrion";
        public const string GETEMPLOYEE = "SP_getAllEmployee";

        public const string GET_WEB_MODULE = "SP_GetWebModule";

        public const string GETTYPEMASTER = "SP_getTypeMaster";
        public const string GETCITYDD = "SP_getCityDD";

        //Employee 
        public const string INSERTUPDATEEMPLOYEEDETAILS = "Sp_InsertUpdate_EmployeeDetails";
        public const string GET_EMPLOYEE_DETAILS = "SP_getEmployeeDetails";
        public const string GET_EMPLOOYEE_DD = "SP_GetEmplooyeeDD";


        //Student 
        public const string INSERTUPDATESTUDENTDETAILS = "Sp_InsertUpdate_StudentDetails";
        public const string GETSTUDENTDD = "SP_GetStudentDD";
        public const string GETSTUDENTDETAILS = "SP_GetStudentDetails";
        
        
        //Teacher
        public const string INSERTUPDATE_TEACHERDETAILS = "Sp_InsertUpdate_TeacherDetails";
        public const string GETTEACHERDD = "SP_GetTeacherDD";
        
        public const string GET_TEACHER_DETAILS = "SP_GetTeacherDetails";

        //master
        public const string SYSMaster = "sp_SYSMaster";
        public const string GET_FORM_MASTER = "Sp_GetFormMaster";

        //Attendence
        public const string ManageStudentAttendenceLog = "SP_ManageStudentAttendenceLog";
        public const string Get_Today_Student_Attendence = "GetTodayStudentAttendence";

        //generic 20-10-2023
        public const string GET_MODULE = "getModule";

        //Exam
        public const string ManageCourseMaster = "SP_ManageCourseMaster";
        public const string ManageMCQQS = "SP_ManageMCQQS";
        public const string ManageMCQCourseDetail = "SP_ManageMCQCourseDetail";
        public const string ManageStudentMCQResult = "SP_ManageStudentMCQResult";
        public const string SubmitStudentMCQQuestion = "SP_SubmitStudentMCQQuestion";


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.DBObjects
{
    public class SqlQry
    {
        public const string GetALLEmployeeList = " Select * from Hrdmast";
        public const string GetEmployeeDD = "select Emp_Code as Type_Code, Emp_FName+Emp_MName+Emp_LName as Type_Name from EmployeeDetails";
        //public const string EmployeeDetails = "select * from EmployeeDetails";
        public const string GetFormMaster = "SELECT Id, COLUMN_ID, COLUMN_NAME, PRINT_NAME, DATATYPE, IS_NULLABLE, CHARACTER_LENGTH, TABLE_NAME FROM FORM_MASTER";

        //Student
        public const string StudentList = "SELECT Id, StudentId, StudentName, DOB, Gender, StudentEmail, StudentPhone, Address1, Address2, FathersName, MothersName, GurdianName, EmergencyContact, Natinality, StudentBloodGroup, MedicalHistory, CreatedDate,LastUpdatedDate, IsActive FROM StudentDetails  where IsActive=1";
        public const string GetStudentName = "Select StudentId  as Type_Code,StudentName as Type_Name from StudentDetails where IsActive=1 and StudentId= '%'+@User_Code'%' or  StudentName='%'+@User_Code'%'";
        public const string GetBloodGroopDD = "Select Id  as Type_Code,BloodGroup as Type_Name from BloodGroupMaster where IsActive=1";
        public const string GetClassDD = "SELECT ClassId as Type_Code, ClassName as Type_Name FROM ClassMast where IsActive =1";
        public const string GetSectionDD = "SELECT SectionId as Type_Code, SectionName as Type_Name FROM SectionMast where IsActive =1";

        //Teacher
        public const string GetTeacherDetails = "SELECT Id, TeacherId, TeacherFName, TeacherLName, DOB, Gender, JoiningDate, SubjectTaught, Department, Qualification, Salary, TeacherEmail, TeacherPhone, Address1, Address2, City, State, Country, PostalCode, FathersName, MothersName, EmergencyContact, Natinality, TeacherBloodGroup, MedicalHistory, CreatedDate, LastUpdatedDate, Imageupload, IsActive FROM TeacherDetails where IsActive=1";

        //Attendence
       

    }
}

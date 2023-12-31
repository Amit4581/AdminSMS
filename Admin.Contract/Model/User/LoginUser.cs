namespace Admin.Contract.Models.User
{
    public class LoginUser
    {
        public int Id { get; set; }
        public string User_Code { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phone { get; set; }
        public string Email_Address { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Role { get; set; }
        public string COMP_CODE { get; set; }
    }
}

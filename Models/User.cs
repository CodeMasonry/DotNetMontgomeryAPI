namespace MontgomeryAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set;} = "";
        public string Email { get; set;} = "";
        public int SubAgencyId {get; set;}
        public string Gender { get; set;} = "";
        public bool Active { get; set;}
        public string FirstName { get; set;} = "";
        public string LastName { get; set;} = "";

        //located on different tables
        public decimal Salary { get; set;} //UserSalary -> SubAgency -> Agency
        public int JobTitleId { get; set;} //UserJobInfo -> SubAgency -> Agency
    }
}
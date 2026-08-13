namespace StenfordAPI.Models
{
	public class LoginModel
	{
		public string UserName { get; set; } = null!;
		public string Password { get; set; } = null!;
	}

	public class LoginResponseModel
	{
		public Guid AspNetUserID { get; set; }
		public string EmailId { get; set; } = null!;
		public string UserName { get; set; } = null!;
		public int RoleId { get; set; }
		public string RoleName { get; set; } = null!;
		public int AdminID { get; set; }
		public int SalesPersonID { get; set; }

        public int AspNetUserWiseRoleId { get; set; }

        public string? Token { get; set; }
	}

    public class ProfileModel
    {
        public int? SalesPersonId { get; set; }
        public int? AdminId { get; set; }
        public string Name { get; set; } = null!;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? PrimaryContact { get; set; }
        public string? State { get; set; }
        public int? TotalVisits { get; set; }
        public int? ShowroomCount { get; set; }
        public int? ThisMonthVisits { get; set; }
    }
}

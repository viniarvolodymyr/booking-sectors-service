namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class RegistrationDTO
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int CreateUserId { get; set; }
        public int? ModUserId { get; set; }
        public string Photo { get; set; }
    }
}

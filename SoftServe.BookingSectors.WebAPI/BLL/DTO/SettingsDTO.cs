namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class SettingsDTO
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int CreateUserId { get; set; }
        public int? ModUserId { get; set; }
    }
}

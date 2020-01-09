namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class SettingsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int CreateUserId { get; set; }
        public int? ModUserId { get; set; }
    }
}

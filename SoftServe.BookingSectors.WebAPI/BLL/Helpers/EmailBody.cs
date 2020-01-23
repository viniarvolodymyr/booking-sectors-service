namespace SoftServe.BookingSectors.WebAPI.BLL.Helpers
{
    public static class EmailBody
    {
 
        public static class Registration
        {
           

            public static string GetBodyMessage(string userName, string login, string confirmEmailLink)
            {
                return 
                    $"Hello, {userName}. <br> " +
                    $"You write a site for Booking Fishing sectors. <br>" +
                    $" Your login: {login} <br> " +
                    $"Confirm your email: " +
                    $"<a href=\"{confirmEmailLink}\">Confirm<a> <br>" +
                    $"Have a nice day :)";
            }
        }
    }
}

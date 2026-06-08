namespace JWTToeknDemo.Models
{
    public class User
    {
       public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? Created_date { get; set; }
    }
}

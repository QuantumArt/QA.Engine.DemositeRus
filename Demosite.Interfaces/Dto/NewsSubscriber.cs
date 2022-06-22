namespace Demosite.Interfaces.Dto
{
     public class NewsSubscriber
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }
        public string Activity { get; set; }
        public string Email { get; set; } = "";
        public int[] NewsCategory { get; set; }
        public bool IsActive { get; set; } = false;
    }
}

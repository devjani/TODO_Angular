namespace TODO.Model.Common
{
    public class RecipientUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
    }
}

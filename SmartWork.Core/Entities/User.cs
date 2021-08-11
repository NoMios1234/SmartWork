using Microsoft.AspNetCore.Identity;

namespace SmartWork.Core.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
    }
}
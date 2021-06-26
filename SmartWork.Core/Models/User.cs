using Microsoft.AspNetCore.Identity;

namespace SmartWork.Core.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
    }
}
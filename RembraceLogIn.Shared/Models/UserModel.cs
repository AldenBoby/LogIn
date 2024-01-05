using System.ComponentModel.DataAnnotations;

namespace RembraceLogIn.Shared.Models
{
    public class UserModel //attributes of the user to be displayed. Obviously don't want to display password (even though it would be hashed)
    {
        public string UserName { get; set; } 
        public string Email { get; set;}
    }
}

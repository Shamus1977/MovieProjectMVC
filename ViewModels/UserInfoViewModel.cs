using L8HandsOn.Models;

namespace L8HandsOn.ViewModels
{
    public class UserInfoViewModel
    {
        public User? User { get; set; }
        public List<MovieWatched>? Movies { get; set; }
        public UserInfoViewModel(User user, List<MovieWatched> movies)
        {
            User = user;
            User.Email = user.Email;
            User.UserName = user.UserName;
            User.EmailConfirmed = user.EmailConfirmed;
            User.PhoneNumber = user.PhoneNumber;
            User.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            User.TwoFactorEnabled = user.TwoFactorEnabled;

            Movies = movies;
        }
    }
}

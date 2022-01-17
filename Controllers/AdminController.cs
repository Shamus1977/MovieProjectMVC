using L8HandsOn.Data;
using L8HandsOn.Models;
using L8HandsOn.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L8HandsOn.Controllers
{
    public class AdminController : Controller
    {

        public RoleManager<IdentityRole> RoleManager { get; }
        public UserManager<User> UserManager { get; }
        public ApplicationDbContext _context;
        
        public AdminController(RoleManager<IdentityRole> roleManager, 
                    UserManager<User> userManager, ApplicationDbContext context)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = UserManager.Users;
            return View(users);
        }

        public async Task<IActionResult> UsersMovieList(string userId)
        {
            User user = await UserManager.FindByIdAsync(userId);
            
            if (_context.WatchedMovies != null && user != null)
            {
                string userEmail = user.Email;
                var movies = await _context.WatchedMovies.Where(m => m.Email == userEmail).GroupBy(m => new { m.Title }).Select(g => g.FirstOrDefault()).ToListAsync();

                UserInfoViewModel model = new UserInfoViewModel(user, movies!);
                return View(model);
            }
            if (user == null)
            {
                return NotFound();
            }
            return View("GetUsers");
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = model.RoleName
                };
                IdentityResult result = await RoleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles", "Admin");   
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View("Error");
            }
            var model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName=role.Name,
                
            };
            foreach(var user in UserManager.Users)
            {
                if(await UserManager.IsInRoleAsync(user, role.Name))
                {
                    model?.UsersInRoles?.Add(user.UserName);
                };
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return View("Error");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await RoleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }

        public IActionResult GetRoles()
        {
            var roles = RoleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.RoleId = roleId;
            var role = await RoleManager.FindByIdAsync(roleId);

            if(role == null)
            {
                return View("Error");
            }
            else
            {
                var model = new List<UserRoleViewModel>();

                foreach(var user in UserManager.Users)
                {
                    var userRoleViewModel = new UserRoleViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };

                    if(await UserManager.IsInRoleAsync(user, role.Name))
                    {
                        userRoleViewModel.IsSelected = true;
                    }
                    else
                    {
                        userRoleViewModel.IsSelected = false;
                    }
                    model.Add(userRoleViewModel);
                }
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {

            var role = await RoleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return View("Error");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await UserManager.FindByIdAsync(model[i].UserId);


                IdentityResult? result = null;
                if (model[i].IsSelected && !(await UserManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await UserManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await UserManager.IsInRoleAsync(user, role.Name))
                {
                    result = await UserManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role =await RoleManager.FindByIdAsync(roleId);
            if(role == null)
            {
                return View("Error");
            }
            else
            {
                var result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("GetRoles");
            }   
        }
    }
}

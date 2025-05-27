using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admin")]
    public class RegisterNewUsersDataModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        [BindProperty]
        public RegisterNewUserVm NewUserVm { get; set; } = default!;

        [TempData]
        public string StatusMessage { get; set; } = default!;

        public RegisterNewUsersDataModel(
            UserManager<User> userManager,
            ILogger logger,
            ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            var parent = _dbContext.Roles.FirstOrDefault(x => x.RoleValue == RoleValue.Parent);

            ViewData["Roles"] = new SelectList(
                _dbContext.Roles.Select(t => new
                {
                    Text = t.Name,
                    Value = t.Id
                }), "Value", "Text", parent?.Id);

            ViewData["Groups"] = new SelectList(
                _dbContext.Groups.Select(t => new
                {
                    Text = t.Name,
                    Value = t.Id
                }), "Value", "Text");

            ViewData["Parents"] = new SelectList(
                _dbContext.Users.OfType<Parent>().Select(t => new
                {
                    Text = $"{t.FirstName} {t.LastName}",
                    Value = t.Id
                }), "Value", "Text");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var tupleUserRole = CreateUserBasedOnRole(NewUserVm);
                var result = await _userManager.CreateAsync(tupleUserRole.Item1, NewUserVm.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    result = await _userManager.AddToRoleAsync(tupleUserRole.Item1, tupleUserRole.Item2.Name);

                    if (result.Succeeded)
                    {
                        OnGet(); // reload lists
                        StatusMessage = "User created";
                        return RedirectToPage();
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            OnGet(); // reload lists in case of failure
            return RedirectToPage();
        }

        private Tuple<User, Role> CreateUserBasedOnRole(RegisterNewUserVm inputModel)
        {
            var role = _dbContext.Roles.FirstOrDefault(r => r.Id == inputModel.RoleId);

            if (role == null)
                throw new InvalidOperationException("Role not exists.");

            return role.RoleValue switch
            {
                RoleValue.Student => new Tuple<User, Role>(_mapper.Map<Student>(inputModel), role),
                RoleValue.Parent => new Tuple<User, Role>(_mapper.Map<Parent>(inputModel), role),
                RoleValue.Teacher => new Tuple<User, Role>(_mapper.Map<Teacher>(inputModel), role),
                RoleValue.Admin or RoleValue.User => new Tuple<User, Role>(_mapper.Map<User>(inputModel), role),
                _ => throw new ArgumentException("Role not matched.")
            };
        }
    }
}

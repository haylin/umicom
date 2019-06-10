

using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Umicom.Passport.Quickstart.UI
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IHostingEnvironment _environment;

        public HomeController(IIdentityServerInteractionService interaction, IHostingEnvironment environment)
        {
            _interaction = interaction;
            _environment = environment;
        }

        public IActionResult Index()
        {
            if (_environment.IsDevelopment())
            {
                // only show in development
                return View();
            }

            return NotFound();
        }

        /// <summary>
        /// 显示错误页面
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // 跳转到IdentityServer错误页面.
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;

                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }

            return View("Error", vm);
        }
    }
}
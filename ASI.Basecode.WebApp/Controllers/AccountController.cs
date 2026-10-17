using ASI.Basecode.WebApp.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace ASI.Basecode.WebApp.Controllers
{
    /// <summary>
    /// Placeholder auth controller (auth is intentionally stripped per AGENTS.md).
    /// Views are Figma-faithful static pages; forms are non-functional.
    /// </summary>
    public class AccountController : ControllerBase<AccountController>
    {
        public AccountController(
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IMapper mapper)
            : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
        }

        public IActionResult Login() => this.View();
        public IActionResult Register() => this.View();
        public IActionResult ForgotPassword() => this.View();
        public IActionResult ResetPassword() => this.View();
    }
}

using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ViewModelClasses;
using ASI.Basecode.WebApp.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace ASI.Basecode.WebApp.Controllers
{
    public class NotificationsController : ControllerBase<NotificationsController>
    {
        private readonly ISettingsService _service;

        public NotificationsController(
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IMapper mapper,
            ISettingsService service)
            : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            this._service = service;
        }

        public IActionResult Index()
        {
            var userId = this._httpContextAccessor.HttpContext?.Session?.GetString("CurrentUserId");
            long.TryParse(userId, out var id);
            if (id == 0)
            {
                this.TempData["ErrorMessage"] = "Select an operator first.";
                return this.RedirectToAction("Index", "Operator");
            }
            return this.View(this._service.GetNotifications(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkRead(long id)
        {
            var userId = this._httpContextAccessor.HttpContext?.Session?.GetString("CurrentUserId");
            long.TryParse(userId, out var uid);
            this._service.MarkNotificationRead(id, uid);
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAllRead()
        {
            var userId = this._httpContextAccessor.HttpContext?.Session?.GetString("CurrentUserId");
            long.TryParse(userId, out var uid);
            this._service.MarkAllNotificationsRead(uid);
            this.TempData["SuccessMessage"] = "All notifications marked as read.";
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

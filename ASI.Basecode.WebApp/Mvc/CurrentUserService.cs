using ASI.Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace ASI.Basecode.WebApp.Mvc
{
    /// <summary>
    /// Stand-in operator identity until web login/auth is reintroduced.
    /// Reads the current user id from session; a future "select operator" admin action
    /// will set it. When nothing is set, audit entries are written with a null actor.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private const string ActorUserIdSessionKey = "CurrentUserId";
        private const string ActorNameSessionKey = "CurrentUserName";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public long? GetActorUserId()
        {
            var raw = this._httpContextAccessor.HttpContext?.Session?.GetString(ActorUserIdSessionKey);
            return long.TryParse(raw, out var id) ? id : null;
        }

        public string GetActorName()
        {
            return this._httpContextAccessor.HttpContext?.Session?.GetString(ActorNameSessionKey);
        }
    }
}

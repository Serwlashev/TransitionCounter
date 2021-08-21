using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using TransitionCounter.Services.Interfaces;

namespace TransitionCounter.Services
{
    public class SessionService : ISessionService
    {
        IHttpContextAccessor _accessor;

        public SessionService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public void CountTransition(string key)
        {
            if (_accessor.HttpContext.Session.Keys.Contains(key))
            {
                int transitions = _accessor.HttpContext.Session.GetInt32(key).Value;
                transitions += 1;
                _accessor.HttpContext.Session.SetInt32(key, transitions);
            }
            else
            {
                _accessor.HttpContext.Session.SetInt32(key, 1);
            }
        }

        public int GetTransitionNumber(string key)
            => _accessor.HttpContext.Session.GetInt32(key).Value;
    }
}
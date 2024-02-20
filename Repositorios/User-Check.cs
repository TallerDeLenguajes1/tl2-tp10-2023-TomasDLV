using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_proyecto_TomasDLV.Repositorios;

    public class UserCheck
    {
        private IHttpContextAccessor accessor;

        public UserCheck(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public bool IsAdmin() => accessor.HttpContext.Session.GetInt32("rol") == 1;
        public bool NotLogged() => string.IsNullOrEmpty(accessor.HttpContext.Session.GetString("usuario"));
        public int LoggedUserId() => Convert.ToInt32(accessor.HttpContext.Session.GetString("id"));
        public string LoggedUserName() => accessor.HttpContext.Session.GetString("usuario");
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_proyecto_TomasDLV.Repositorios;

    public class UserCheck
    {
        public IHttpContextAccessor accessor;
        public int Id {get;set;}
        public UserCheck(IHttpContextAccessor Accessor)
        {
            this.accessor = Accessor;
            
        }

        public bool IsAdmin() => accessor.HttpContext.Session.GetInt32("rol") == 1;
        public bool NotLogged() => string.IsNullOrEmpty(accessor.HttpContext.Session.GetString("usuario"));
        public int? LoggedUserId() => accessor.HttpContext.Session.GetInt32("id");
        public string LoggedUserName() => accessor.HttpContext.Session.GetString("usuario");
    }

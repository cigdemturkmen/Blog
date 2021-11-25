using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogAdmin.Controllers
{
    [Authorize] // auth olmadan çalışırsa user null gelir. bunu koymazsan aşağıda null check yapman lazım. şimdi basecontrollerı inherit eden classlar otomatikmen auth attributeü eklenmiş olarak kabul edilir.
    public class BaseController : Controller
    {
        public int GetCurrentUserId()
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            //var id = Convert.ToInt32(HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value); made nullable

            return id;
        }
    }
}

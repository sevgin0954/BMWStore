using BMWStore.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    [Area(WebConstants.AdminAreaName)]
    [Authorize(Roles = WebConstants.AdminRoleName)]
    public abstract class BaseAdminController : Controller
    {

    }
}
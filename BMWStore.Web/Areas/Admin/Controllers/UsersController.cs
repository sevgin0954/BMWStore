using BMWStore.Common.Enums;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly IAdminUsersService adminUsersService;

        public UsersController(IAdminUsersService adminUsersService)
        {
            this.adminUsersService = adminUsersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(UserSortStrategy sortStrategyName = UserSortStrategy.Name)
        {
            var sortStrategy = UserSortStrategyFactory.GetStrategy(sortStrategyName);
            var models = await this.adminUsersService.GetAllUsersAsync(sortStrategy);

            return View(models);
        }
    }
}
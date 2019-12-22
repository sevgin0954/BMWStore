using BMWStore.Entities;
using BMWStore.Services.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
	public interface ICarTestDriveService
	{
		Task<TModel> GetCarTestDriveModelById<TModel>(string id, ClaimsPrincipal user)
			where TModel : BaseCarTestDriveServiceModel;

		Task<IQueryable<TModel>> GetCarTestDriveModelAsync<TModel>(
			IQueryable<BaseCar> cars,
			ClaimsPrincipal user,
			int pageNumber) where TModel : BaseCarTestDriveServiceModel;
	}
}

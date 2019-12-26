using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Services.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Services.SortStrategies.UserStrategies.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BMWStore.Tests.Common.MockMethods
{
	public static class CommonSetupMockMethods
	{
		public static void SetupMockedUserManagerGetUserId(Mock<UserManager<User>> mock, string userId)
		{
			mock.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
				.Returns(userId);
		}

		public static void SetupMockedSignInManagerIsSignIn(Mock<SignInManager<User>> mock, bool isSignIn)
		{
			mock.Setup(sm => sm.IsSignedIn(It.IsAny<ClaimsPrincipal>()))
				.Returns(isSignIn);
		}

		public static void SetupMockedUserManagerIsInRoleAsync(Mock<UserManager<User>> mock, bool returns)
		{
			mock.Setup(um => um.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
				.Returns(Task.FromResult(returns));
		}

		public static void SetupMockedDistributedCacheGetAsync(Mock<IDistributedCache> mock, string key, object obj)
		{
			var objAsByteArray = JSonHelper.Serialize(obj);
			if (obj == null)
			{
				objAsByteArray = null;
			}
			mock.Setup(m => m.GetAsync(key, It.IsAny<CancellationToken>()))
				.Returns(Task.FromResult(objAsByteArray));
		}

		public static void SetupMockedUserSortStrategy(Mock<IUserSortStrategy> mock)
		{
			mock.Setup(uss => uss.Sort(It.IsAny<IQueryable<User>>()))
				.Returns<IQueryable<User>>(users => users);
		}

		public static void SetupTestDriveSortStrategy(Mock<ITestDriveSortStrategy> mock)
		{
			mock.Setup(tdss => tdss.Sort(It.IsAny<IQueryable<TestDrive>>()))
				.Returns<IQueryable<TestDrive>>(testDrives => testDrives);
		}
	}
}
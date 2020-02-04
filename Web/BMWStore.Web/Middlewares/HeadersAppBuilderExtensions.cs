using Microsoft.AspNetCore.Builder;

namespace BMWStore.Web.Middlewares
{
	public static class HeadersAppBuilderExtensions
	{
		public static IApplicationBuilder UseResponseXFrame(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ResponseXFrameMiddleware>();
		}
	}
}

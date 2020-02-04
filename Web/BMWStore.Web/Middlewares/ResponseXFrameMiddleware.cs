using BMWStore.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace BMWStore.Web.Middlewares
{
	public class ResponseXFrameMiddleware
	{
		private readonly RequestDelegate next;
		private readonly IConfiguration configuration;

		public ResponseXFrameMiddleware(RequestDelegate next, IConfiguration configuration)
		{
			this.next = next;
			this.configuration = configuration;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var xframeOptionValue = this.configuration.GetSection("Security:XFrameOptionsValue").Value;
			context.Response.Headers.Add(WebConstants.XFrameOptionsHeader, new StringValues(xframeOptionValue));

			await this.next(context);
		}
	}
}

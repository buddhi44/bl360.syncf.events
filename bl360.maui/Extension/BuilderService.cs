using bl360.clientInfrastructure;
using bl360.clientInfrastructure.Authentication;
using bl360.clientInfrastructure.Routes;
using bl360.clientInfrastructure.Services;
using bl360.maui.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using System.Globalization;

namespace bl360.maui.Extension
{
	public static class BuildServices
	{
		private const string ClientName = "MSE";
		public static void BuildAddtionals(this IServiceCollection Services)
		{
			Services
				.AddAuthorizationCore(options =>
				{
					// RegisterPermissionClaims(options);
				})
				.AddScoped<BL10AuthProvider>()
				.AddScoped<AuthenticationStateProvider, BL10AuthProvider>()
				.AddScoped<IStorageService, SecureStorageService>()
		        .AddManagers()
				.AddTransient<AuthenticationHeaderHandler>()
				.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
				.CreateClient(ClientName).EnableIntercept(sp))
				.AddHttpClient(ClientName, client =>
				{
					client.DefaultRequestHeaders.AcceptLanguage.Clear();
					client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);
					client.BaseAddress = new Uri(BaseEndpoint.BaseURL);
					client.Timeout = TimeSpan.FromMinutes(30);
				})
				.AddHttpMessageHandler<AuthenticationHeaderHandler>();
			    Services.AddHttpClientInterceptor();
		}


		public static IServiceCollection AddManagers(this IServiceCollection services)
		{
			var managers = typeof(IManager);

			var types = managers
				.Assembly
				.GetExportedTypes()
				.Where(t => t.IsClass && !t.IsAbstract)
				.Select(t => new
				{
					Service = t.GetInterface($"I{t.Name}"),
					Implementation = t
				})
				.Where(t => t.Service != null);

			foreach (var type in types)
			{

				if (managers.IsAssignableFrom(type.Service))
				{
					services.AddTransient(type.Service, type.Implementation);
				}
			}

			return services;
		}
	}
}

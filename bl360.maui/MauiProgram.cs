using bl360.maui.Extension;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;

namespace bl360.maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSyncfusionBlazor();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCfEx3RHxbf1x0ZFBMYF5bRnBPMyBoS35RckViWH1edXRXRGdbUkZ3");
			builder.Services.AddTransient<HttpClient>();

			builder.Services.AddAuthorizationCore(options =>
			{
				options.AddPolicy("SeniorEmployee", policy =>
					policy.RequireClaim("IsUserEmployedBefore1990", "true"));
			});
			builder.Services.BuildAddtionals();
#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

using AutoMapper;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Factories.Matches;
using EmpoweredPixels.Factories.Rewards;
using EmpoweredPixels.Hubs.Matches;
using EmpoweredPixels.Jobs;
using EmpoweredPixels.Jobs.Rewards;
using EmpoweredPixels.Models;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Providers.Version;
using EmpoweredPixels.Utilities.ContributionPointCalculation;
using EmpoweredPixels.Utilities.EloCalculation;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EmpoweredPixels
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddHangfire(o => o.UseSqlServerStorage(Configuration.GetConnectionString()));
      services.AddHangfireServer();

      services.AddResponseCompression();

      services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
      services.AddSingleton<IVersionProvider, VersionProvider>();
      services.AddTransient<IEngineFactory, EngineFactory>();
      services.AddTransient<ILeagueJob, LeagueJob>();
      services.AddTransient<ILoginRewardJob, LoginRewardJob>();
      services.AddSingleton<IRewardFactory, RewardFactory>();
      services.AddSingleton<IContributionPointCalculator, ContributionPointCalculator>();
      services.AddSingleton<IEloCalculator, EloCalculator>();

      services.AddDbContextPool<DatabaseContext>(o => o.ConfigureDatabase(Configuration));
      services.AddAutoMapper(typeof(Startup));

      services.AddSignalR();

      services.AddAuthentication(o =>
      {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(o =>
      {
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Configuration.GetSigningKey()),
          ValidateIssuer = false,
          ValidateAudience = false,
        };
      });

      // In production, the Angular files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/dist";
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
      IApplicationBuilder app,
      IHostingEnvironment env,
      IRecurringJobManager recurringJobManager,
      DatabaseContext databaseContext)
    {
      recurringJobManager.AddLeagueJobs(databaseContext);
      recurringJobManager.AddLoginRewardJob();

      app.UseResponseCompression();
      app.UseAuthentication();
      app.UseStaticFiles();
      app.UseSpaStaticFiles();

      if (env.IsDevelopment())
      {
        app.UseHangfireDashboard();
      }

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller}/{action=Index}/{id?}");
      });

      app.UseSignalR(configure =>
      {
        configure.MapHub<MatchHub>("/hub/match");
      });

      app.UseSpa(spa =>
      {
        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        // see https://go.microsoft.com/fwlink/?linkid=864501
        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseAngularCliServer(npmScript: "start");
        }
      });
    }
  }
}

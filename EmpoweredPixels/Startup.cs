using AutoMapper;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Factories.Matches;
using EmpoweredPixels.Factories.Rewards;
using EmpoweredPixels.Jobs;
using EmpoweredPixels.Jobs.Rewards;
using EmpoweredPixels.Jobs.Seasons;
using EmpoweredPixels.Models;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Providers.Version;
using EmpoweredPixels.Utilities.ContributionPointCalculation;
using EmpoweredPixels.Utilities.EloCalculation;
using EmpoweredPixels.Utilities.EnhancementCalculation;
using EmpoweredPixels.Utilities.EquipmentGeneration;
using EmpoweredPixels.Utilities.FighterAttunementSelector;
using EmpoweredPixels.Utilities.FighterEquipment;
using EmpoweredPixels.Utilities.FighterProgress;
using EmpoweredPixels.Utilities.FighterSkillSelection;
using EmpoweredPixels.Utilities.FighterStatCalculation;
using EmpoweredPixels.Utilities.LeageExecution;
using EmpoweredPixels.Utilities.MatchExecution;
using EmpoweredPixels.Utilities.RewardTrackCalculation;
using EmpoweredPixels.Utilities.Season.Postprocessing;
using EmpoweredPixels.Utilities.Season.Preprocessing;
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
      services.AddScoped<IEngineFactory, EngineFactory>();
      services.AddScoped<ILeagueJob, LeagueJob>();
      services.AddScoped<ILoginRewardJob, LoginRewardJob>();
      services.AddSingleton<IRewardFactory, RewardFactory>();
      services.AddSingleton<IContributionPointCalculator, ContributionPointCalculator>();
      services.AddSingleton<IEloCalculator, EloCalculator>();
      services.AddSingleton<IEnhancementProbability, EnhancementProbability>();
      services.AddSingleton<IEquipmentGenerator, EquipmentGenerator>();
      services.AddSingleton<IFighterOutfitter, FighterOutfitter>();
      services.AddSingleton<IFighterExperienceCalculator, FighterExperienceCalculator>();
      services.AddSingleton<IFighterLevelUpHandler, FighterLevelUpHandler>();
      services.AddSingleton<IFighterSkillSelector, FighterSkillSelector>();
      services.AddSingleton<IFighterStatCalculator, FighterStatCalculator>();
      services.AddScoped<IRewardTrackCalculator, RandomRewardCalculator>();
      services.AddSingleton<IEquipmentEnhancer, EquipmentEnhancer>();
      services.AddSingleton<IEquipmentSalvager, EquipmentSalvager>();
      services.AddScoped<IMatchScoreProcessor, MatchScoreProcessor>();
      services.AddSingleton<IMatchFighterPreparer, MatchFighterPreparer>();
      services.AddScoped<IMatchContributionProcessor, MatchContributionProcessor>();
      services.AddScoped<IMatchFighterEloProcessor, MatchFighterEloProcessor>();
      services.AddScoped<IMatchFighterExperienceProcessor, MatchFighterExperienceProcessor>();
      services.AddScoped<IMatchFighterRewardTrackProcessor, MatchFighterRewardTrackProcessor>();
      services.AddScoped<IMatchLogProcessor, MatchLogProcessor>();
      services.AddScoped<IMatchProcessor, MatchProcessor>();
      services.AddScoped<IMatchPostprocessor, MatchPostprocessor>();
      services.AddScoped<IMatchExecutor, MatchExecutor>();
      services.AddSingleton<ILeagueDivisionDivider, EqualPartLeagueDivisionDivider>();
      services.AddScoped<ILeagueExecutor, LeagueExecutor>();
      services.AddSingleton<IFighterAttunementSelector, FighterAttunementSelector>();

      services.AddScoped<ISeasonMatchRemover, SeasonMatchRemover>();
      services.AddScoped<ISeasonFighterResetter, SeasonFighterResetter>();
      services.AddScoped<ISeasonSalvageRewardProvider, SeasonSalvageRewardProvider>();
      services.AddScoped<ISeasonEloRewardProvider, SeasonEloRewardProvider>();
      services.AddScoped<ISeasonRewardClaimer, SeasonRewardClaimer>();
      services.AddScoped<ISeasonPostprocessor, SeasonPostprocessor>();

      services.AddScoped<ISeasonCreator, SeasonCreator>();
      services.AddScoped<ISeasonInitiatorJob, SeasonInitiatorJob>();
      services.AddScoped<ISeasonUserJob, SeasonUserJob>();

      services.AddDbContext<DatabaseContext>(o => o.ConfigureDatabase(Configuration));
      services.AddAutoMapper(typeof(Startup));

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
      recurringJobManager.AddSeasonJob();

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

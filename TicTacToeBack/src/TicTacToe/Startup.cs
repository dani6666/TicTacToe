using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TicTacToe.Core.Interfaces.Repositories;
using TicTacToe.Core.Interfaces.Services;
using TicTacToe.Core.Services;
using TicTacToe.Repository;
using TicTacToe.Repository.Repositories;

namespace TicTacToe
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
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicTacToe", Version = "v1" });
            });
            services.AddCors();

            //services.AddDbContext<GameContext>(options =>
            //{
            //    options.UseInMemoryDatabase("GamesDatabase");
            //});

            services.AddTransient(_  =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<GameContext>();

                optionsBuilder.UseInMemoryDatabase("GamesDatabase");

                return optionsBuilder.Options;
            });

            services.AddTransient<IDbContextFactory<GameContext>, GameContextFactory>();

            services.AddSingleton<IGamesRepository, GamesRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<IPlayersService, PlayersService>();
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicTacToe v1"));

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

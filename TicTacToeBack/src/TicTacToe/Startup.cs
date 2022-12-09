using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using TicTacToe.Core.Interfaces.Repositories;
using TicTacToe.Core.Interfaces.Services;
using TicTacToe.Core.Model.Entities;
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

            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var database = Environment.GetEnvironmentVariable("DB_NAME");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var connectionString =
                $"Server={host};Port={port};Database={database};Uid={user};Pwd={password};";

            services.AddDbContextFactory<GameContext>(options =>
                //options.UseSqlite("temp.db"));
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddSingleton<IGamesRepository, GamesRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<IPlayersService, PlayersService>();
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var retryCount = 0;
            while (retryCount < 18)
            {
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                    {
                        var context = serviceScope.ServiceProvider.GetRequiredService<GameContext>();
                        context.Database.Migrate();
                    }
                }
                catch (MySqlException)
                {
                    Task.Delay(TimeSpan.FromSeconds(10)).GetAwaiter().GetResult();
                    retryCount++;
                    continue;
                }

                break;
            }
            

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

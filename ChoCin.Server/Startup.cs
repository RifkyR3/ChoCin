using ChoCin.Entities;
using ChoCin.Entities.Helpers;
using ChoCin.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace ChoCin.Server
{
    public class Startup
    {
        public Startup(IServiceCollection services)
        {
            this.ConfigureServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);

            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }
    }
}

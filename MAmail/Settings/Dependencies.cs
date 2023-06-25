using MAmail.Data;
using MAmail.Repositories;
using MAmail.Services;
using MAmail.Utils;

namespace MAmail.Settings
{
    public static class Dependencies
    {
        public static void Inject(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<MAmailDBContext>();

            AddServices(builder.Services);
            AddRepositories(builder.Services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<EmailService>();
            services.AddScoped<RecipientService>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<JWT>();
        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
            services.AddScoped<EmailRepository>();
            services.AddScoped<RecipientRepository>();
            services.AddScoped<AuthenticationRepository>();
        }
    }
}

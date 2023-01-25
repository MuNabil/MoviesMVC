namespace MoviesMVC.Registrars.Services;

public class DbRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // For Cookie
        // builder.Services.ConfigureApplicationCookie(options =>
        //     options.LoginPath = "/UserAuthentication/Login"
        // );
    }
}

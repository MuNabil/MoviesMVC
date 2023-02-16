namespace MoviesMVC.Registrars.Services;

public class MvcRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();

        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddSignalR();
    }
}

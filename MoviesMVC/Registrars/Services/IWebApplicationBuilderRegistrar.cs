namespace MoviesMVC.Registrars.Services;

public interface IWebApplicationBuilderRegistrar : IRegistrar
{
    void RegisterServices(WebApplicationBuilder builder);
}

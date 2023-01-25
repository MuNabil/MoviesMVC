namespace MoviesMVC.Registrars.WebApp;

public interface IWebApplicationRegistrar : IRegistrar
{
    void RegisterPipelineComponents(WebApplication app);
}

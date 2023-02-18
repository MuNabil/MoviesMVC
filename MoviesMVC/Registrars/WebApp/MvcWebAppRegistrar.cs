namespace MoviesMVC.Registrars.WebApp;
public class MvcWebAppRegistrar : IWebApplicationRegistrar
{
    public void RegisterPipelineComponents(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapHub<OrderHub>("/hubs/order");
        app.MapHub<MovieHub>("/hubs/movie");
        app.MapHub<MemberHub>("/hubs/member");
        app.MapHub<GenreHub>("/hubs/genre");
        app.MapHub<MessageHub>("/hubs/chat");
        app.MapHub<PresenceHub>("/hubs/presence");

    }
}

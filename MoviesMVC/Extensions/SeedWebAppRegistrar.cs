namespace MoviesMVC.Extensions;

public static class SeedAndMigrateExtention
{
    public static async Task SeedAndMigrate(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var dbContext = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await dbContext.Database.MigrateAsync();

            await Seed.SeedUsers(userManager, roleManager);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Error occurred during migration");
        }
    }
}

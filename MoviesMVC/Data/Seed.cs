namespace MoviesMVC.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var roles = new List<IdentityRole>{
            new IdentityRole {Name = "Admin"},
            new IdentityRole {Name = "User"}
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        var users = new List<AppUser>(){
            new AppUser{Email = "Ahmed@test.com", UserName = "Ahmed@test.com", Name = "Ahmed"},
            new AppUser{Email = "Mohamed@test.com", UserName = "Mohamed@test.com", Name = "Mohamed"},
            new AppUser{Email = "Mahmoud@test.com", UserName = "Mahmoud@test.com", Name = "Mahmoud"}
        };
        foreach (var user in users)
        {
            await userManager.CreateAsync(user, "Pa$$w0rd");

            await userManager.AddToRoleAsync(user, "User");
        }

        // Create a user and add him to Admin and Moderator roles
        var admin = new AppUser { UserName = "admin@test.com", Name = "admin", Email = "admin@test.com" };
        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRoleAsync(admin, "Admin");
    }
}
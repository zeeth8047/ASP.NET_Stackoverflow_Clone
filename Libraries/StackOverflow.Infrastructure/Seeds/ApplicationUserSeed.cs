using StackOverflow.Infrastructure.Entities.Membership;

namespace StackOverflow.Infrastructure.Seeds
{
    public static class ApplicationUserSeed
    {
        public static ApplicationUser[] Users
        {
            get
            {
                var applicationUser = new ApplicationUser
                {
                    Id = Guid.Parse("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                    DisplayName = "Sai Poojith",
                    Email = "saipoojith@gmail.com",
                    NormalizedEmail = "Sriya@GMAIL.COM",
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    PasswordHash = "123456"
                };

                return new ApplicationUser[]
                {
                    applicationUser
                };
            }
        }
    }
}

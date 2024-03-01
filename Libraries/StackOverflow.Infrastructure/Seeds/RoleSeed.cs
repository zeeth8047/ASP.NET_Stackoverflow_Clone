using StackOverflow.Infrastructure.Entities.Membership;

namespace StackOverflow.Infrastructure.Seeds
{
    public static class RoleSeed
    {
        public static Role[] Roles
        {
            get
            {
                return new Role[]
                {
                    new Role
                    {
                        Id = Guid.Parse("2c5e174e-3b0e-446f-86af-483d56fd7210"),
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp =  DateTime.Now.Ticks.ToString()
                    }
                };
            }
        }
    }
}
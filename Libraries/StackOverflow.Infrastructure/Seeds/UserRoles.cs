using StackOverflow.Infrastructure.Entities.Membership;

namespace StackOverflow.Infrastructure.Seeds
{
    public class UserRoleSeed
    {
        public static UserRole[] UserRoles
        {
            get
            {
                return new UserRole[]
                {
                    new UserRole
                    {
                        UserId = Guid.Parse("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                        RoleId = Guid.Parse("2c5e174e-3b0e-446f-86af-483d56fd7210")
                    }
                };
            }
        }
    }
}

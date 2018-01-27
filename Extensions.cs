using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SearchAThing.NETCoreEFUtil
{

    public static partial class Extensions
    {

        public static void ASPNetIdentityPSQLLike(this ModelBuilder builder)
        {
            builder.Entity<IdentityUser<long>>().ToTable("user");
            {
                builder.Entity<IdentityUser<long>>().ToTable("user");
                builder.Entity<IdentityUser<long>>().Property(w => w.Id).HasColumnName("id");
                builder.Entity<IdentityUser<long>>().Property(w => w.AccessFailedCount).HasColumnName("access_failed_count");
                builder.Entity<IdentityUser<long>>().Property(w => w.Email).HasColumnName("email");
                builder.Entity<IdentityUser<long>>().Property(w => w.EmailConfirmed).HasColumnName("email_confirmed");
                builder.Entity<IdentityUser<long>>().Property(w => w.LockoutEnabled).HasColumnName("lockout_enabled");

            }

            {
                builder.Entity<IdentityRole<long>>().ToTable("role");
            }

            builder.Entity<IdentityUserRole<long>>().ToTable("user_role");
            builder.Entity<IdentityUserLogin<long>>().ToTable("user_login");
            builder.Entity<IdentityUserClaim<long>>().ToTable("user_claim");
            builder.Entity<IdentityRoleClaim<long>>().ToTable("role_claim");
            builder.Entity<IdentityUserToken<long>>().ToTable("user_token");
        }

    }

}

using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SearchAThing.NetCoreEFUtil
{

    public static partial class Util
    {

        /// <summary>
        /// lowecase table,field names ( to avoid the need of doublequote table and field names in psql ); use long as pk ; foreign key starting with id_
        /// </summary>        
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
                builder.Entity<IdentityUser<long>>().Property(w => w.LockoutEnd).HasColumnName("lockout_end");
                builder.Entity<IdentityUser<long>>().Property(w => w.NormalizedEmail).HasColumnName("normalized_email");
                builder.Entity<IdentityUser<long>>().Property(w => w.NormalizedUserName).HasColumnName("normalized_username");
                builder.Entity<IdentityUser<long>>().Property(w => w.PasswordHash).HasColumnName("password_hash");
                builder.Entity<IdentityUser<long>>().Property(w => w.PhoneNumber).HasColumnName("phone_number");
                builder.Entity<IdentityUser<long>>().Property(w => w.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
                builder.Entity<IdentityUser<long>>().Property(w => w.SecurityStamp).HasColumnName("security_stamp");
                builder.Entity<IdentityUser<long>>().Property(w => w.TwoFactorEnabled).HasColumnName("twofactor_enabled");
                builder.Entity<IdentityUser<long>>().Property(w => w.UserName).HasColumnName("username");
                builder.Entity<IdentityUser<long>>().Property(w => w.ConcurrencyStamp).HasColumnName("concurrency_stamp");                
            }

            {
                builder.Entity<IdentityRole<long>>().ToTable("role");
                builder.Entity<IdentityRole<long>>().Property(w => w.Id).HasColumnName("id");
                builder.Entity<IdentityRole<long>>().Property(w => w.ConcurrencyStamp).HasColumnName("concurrency_stamp");                
                builder.Entity<IdentityRole<long>>().Property(w => w.Name).HasColumnName("name");
                builder.Entity<IdentityRole<long>>().Property(w => w.NormalizedName).HasColumnName("normalized_name");
            }

            builder.Entity<IdentityUserRole<long>>().ToTable("user_role");
            {
                builder.Entity<IdentityUserRole<long>>().Property(w => w.UserId).HasColumnName("id_user");
                builder.Entity<IdentityUserRole<long>>().Property(w => w.RoleId).HasColumnName("id_role");
            }

            builder.Entity<IdentityUserLogin<long>>().ToTable("user_login");
            {
                builder.Entity<IdentityUserLogin<long>>().Property(w => w.LoginProvider).HasColumnName("login_provider");
                builder.Entity<IdentityUserLogin<long>>().Property(w => w.ProviderKey).HasColumnName("provider_key");
                builder.Entity<IdentityUserLogin<long>>().Property(w => w.ProviderDisplayName).HasColumnName("provider_display_name");
                builder.Entity<IdentityUserLogin<long>>().Property(w => w.UserId).HasColumnName("id_user");
            }

            builder.Entity<IdentityUserClaim<long>>().ToTable("user_claim");
            {
                builder.Entity<IdentityUserClaim<long>>().Property(w => w.Id).HasColumnName("id");
                builder.Entity<IdentityUserClaim<long>>().Property(w => w.ClaimType).HasColumnName("claim_type");
                builder.Entity<IdentityUserClaim<long>>().Property(w => w.ClaimValue).HasColumnName("claim_value");
                builder.Entity<IdentityUserClaim<long>>().Property(w => w.UserId).HasColumnName("id_user");
            }

            builder.Entity<IdentityRoleClaim<long>>().ToTable("role_claim");
            {
                builder.Entity<IdentityRoleClaim<long>>().Property(w => w.Id).HasColumnName("id");
                builder.Entity<IdentityRoleClaim<long>>().Property(w => w.ClaimType).HasColumnName("claim_type");
                builder.Entity<IdentityRoleClaim<long>>().Property(w => w.ClaimValue).HasColumnName("claim_value");
                builder.Entity<IdentityRoleClaim<long>>().Property(w => w.RoleId).HasColumnName("id_role");
            }

            builder.Entity<IdentityUserToken<long>>().ToTable("user_token");
            {
                builder.Entity<IdentityUserToken<long>>().Property(w => w.UserId).HasColumnName("id_user");
                builder.Entity<IdentityUserToken<long>>().Property(w => w.LoginProvider).HasColumnName("login_provider");
                builder.Entity<IdentityUserToken<long>>().Property(w => w.Name).HasColumnName("name");
                builder.Entity<IdentityUserToken<long>>().Property(w => w.Value).HasColumnName("value");
            }
        }

    }

}

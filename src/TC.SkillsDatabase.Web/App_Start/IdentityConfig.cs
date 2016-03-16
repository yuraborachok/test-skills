namespace TC.SkillsDatabase.Web
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Core.Models.DbModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Models;

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // if (true)
            // {
            //    return Task.FromResult(0);
            // }

            // Credentials:
            var credentialUserName = ConfigurationManager.AppSettings["EmailUserName"];
            var sentFrom = ConfigurationManager.AppSettings["EmailSentFrom"];
            var pwd = ConfigurationManager.AppSettings["EmailPassword"];

            // Configure the client:
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailSmtp"]);

            client.Port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create the credentials:
            NetworkCredential credentials = new NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = false;
            client.Credentials = credentials;

            // Create the message:
            var mail = new MailMessage(sentFrom, message.Destination);

            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            // Send:
            return client.SendMailAsync(mail);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new CustomUserStore(context.Get<ApplicationDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, int>(manager)

            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("PhoneCode",
                  new PhoneNumberTokenProvider<User, int>
                  {
                      MessageFormat = "Your security code is: {0}"
                  });

            manager.RegisterTwoFactorProvider("EmailCode",
                new EmailTokenProvider<User, int>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is: {0}"
                });

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<User, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)this.UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    public class CustomUserStore : UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}

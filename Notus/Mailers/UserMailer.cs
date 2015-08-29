using System;
using Mvc.Mailer;

namespace Notus.Mailers
{
    public class UserMailer : MailerBase, IUserMailer
    {
        public UserMailer()
        {
            MasterName = "_Layout";
        }

        public virtual MvcMailMessage Welcome()
        {
            var mailMessage = new MvcMailMessage {Subject = "Welcome"};
            PopulateBody(mailMessage, "Welcome");
            return mailMessage;
        }


        public virtual MvcMailMessage Invite(string email, Guid groupIdToken)
        {
            var mailMessage = new MvcMailMessage {Subject = "Invite"};
            mailMessage.To.Add(email);
            ViewBag.group = "gr:" + groupIdToken;
            PopulateBody(mailMessage, "Invite");
            return mailMessage;
        }

        public virtual MvcMailMessage Support(string email, Guid goalIdToken)
        {
            var mailMessage = new MvcMailMessage {Subject = "Support My Goal"};
            mailMessage.To.Add(email);
            ViewBag.goal = "go:" + goalIdToken;
            PopulateBody(mailMessage, "SupportGoal");
            return mailMessage;
        }

        public virtual MvcMailMessage ResetPassword(string email, Guid passwordResetToken)
        {
            var mailMessage = new MvcMailMessage {Subject = "Reset Password"};
            mailMessage.To.Add(email);
            ViewBag.token = "pwreset:" + passwordResetToken;
            PopulateBody(mailMessage, "PasswordReset");
            return mailMessage;
        }

        public virtual MvcMailMessage InviteNewUser(string email, Guid registrationToken)
        {
            var mailMessage = new MvcMailMessage {Subject = "Invitation to Notus" };
            mailMessage.To.Add(email);
            ViewBag.token = "reg:" + registrationToken;
            PopulateBody(mailMessage, "InviteNewUser");
            return mailMessage;
        }
    }
}
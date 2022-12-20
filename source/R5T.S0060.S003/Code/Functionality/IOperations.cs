using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

using Microsoft.Extensions.Logging;

using R5T.F0032;
using R5T.T0132;
using R5T.T0144;


namespace R5T.S0060.S003
{
    [FunctionalityMarker]
    public partial interface IOperations : IFunctionalityMarker
    {
        public void SendMail(ILogger logger)
        {
            logger.LogInformation("Sending mail...");

            var toAddresses = new[]
            {
                D8S.Z0003.EmailAddresses.Instance.David_Gmail,
            };

            var subject = "Test Email";

            var bodyLines = new[]
            {
                $"\n\nSet by machine: {F0000.MachineNameOperator.Instance.GetMachineName()}"
            };

            var body = R5T.F0000.StringOperator.Instance.Join(
                Environment.NewLine,
                bodyLines);

            // Build email.
            var gmailAuthenticationFilePath = FilePathProvider.Instance.GetGmailAuthenticationFilePath();

            var authentication = JsonOperator.Instance.LoadFromFile_Synchronous<Authentication>(
                gmailAuthenticationFilePath,
                JsonKeys.Instance.GmailAuthentication);

            var fromAddress = authentication.Username;
            var displayName = "David-Automation";

            var emailMessage = new MailMessage
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
                From = new MailAddress(fromAddress, displayName),
            };

            //// Cannot attach the log file, because it is being used by another process. (Currently writing to the log file!) // Attach the log file to the message.
            //// Instead attach the results file?
            //emailMessage.Attachments.Add(
            //    new Attachment(
            //        Instances.FilePaths.LogFilePath));

            toAddresses.ForEach(toAddress => emailMessage.To.Add(new MailAddress(toAddress)));

            using var smtpClient = new SmtpClient()
            {
                Host = @"smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(authentication.Username, authentication.Password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };

            smtpClient.Send(emailMessage);

            logger.LogInformation("Sent mail.");
        }
    }
}

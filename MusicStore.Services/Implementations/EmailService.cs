﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MusicStore.Domain.Configuration;
using MusicStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly SmtpConfiguration _smtp;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<AppConfig> options, ILogger<EmailService> logger)
        {
            _smtp = options.Value.SmtpConfiguration;
            _logger = logger;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mailMessage = new MailMessage(new MailAddress(_smtp.UserName, _smtp.FromName), new MailAddress(email));
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;

                using var smtpClient = new SmtpClient(_smtp.Server, _smtp.PortNumber)//envia servidor y numero de puerto
                {
                    Credentials = new NetworkCredential(_smtp.UserName, _smtp.Password), //configura credenciales de red
                    EnableSsl = _smtp.EnableSsl,
                };

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Se envió correctamente el correo a {email}", email);
            }
            catch (SmtpException ex)
            {
                _logger.LogWarning(ex, "No se puede enviar el correo {message}", ex.Message);
            }

            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error al enviar el correo a {email} {message}", email, ex.Message);

            }
        }
    }
}

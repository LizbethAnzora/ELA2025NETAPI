using System;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Implementaciones;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(string toEmail, string subject, string message)
        {
            Console.WriteLine($"Simulando envío de correo a: {toEmail}");
            Console.WriteLine($"Asunto: {subject}");
            Console.WriteLine($"Mensaje: {message}");
            return Task.CompletedTask;
        }

}

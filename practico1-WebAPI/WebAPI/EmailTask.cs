using System;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.IBLs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI;
using WebAPI.Models;

public class EmailSenderBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IBL_OC _blOC;

    public EmailSenderBackgroundService(IServiceProvider serviceProvider, IBL_OC blOC)
    {
        _serviceProvider = serviceProvider;
        _blOC = blOC;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Lógica para enviar el correo electrónico
            using (var scope = _serviceProvider.CreateScope())
            {
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
                var userId = "aquí proporcionas el user id"; // Obtén el user id de alguna manera

                // Verificar si hay una orden activa para el usuario
                var ordenes = _blOC.ObtenerOCPorUserId(userId);

                foreach (var orden in ordenes)
                {
                    if (orden != null && orden.EstadoOrden == "ACTIVA")
                    {
                        // Enviar el correo electrónico
                        await emailSender.SendEmailAsync("federicodn3@gmail.com", "Revisa tu Carrito!", "Tienes cosas en tu carrito por comprar");
                        break;  // Si se encuentra una orden activa, puedes salir del bucle
                    }
                }
            }

            // Espera 24 horas antes de ejecutar la tarea nuevamente
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}


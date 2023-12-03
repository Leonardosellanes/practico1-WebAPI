using Envios_Tiempo;
using Microsoft.AspNetCore.Mvc;
using TiemposEntrega;

[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    private List<Warehouse> warehouses = new List<Warehouse>
    {
        new Warehouse { Id = 1, Nombre = "Warehouse A", Diasentregadefault = 2, Direccion = "Dirección del Warehouse A" },
        new Warehouse { Id = 2, Nombre = "Warehouse B", Diasentregadefault = 3, Direccion = "Dirección del Warehouse B" },
        // Agregar más almacenes con información de dirección
    };

    [HttpPost("calculate")]
    public IActionResult CalculateDeliveryTime([FromBody] DeliveryRequest deliveryRequest)
    {
        // Simular una demora para la API de prueba
        //Task.Delay(1000).Wait(); // Puedes usar Task.Delay sin bloquear el hilo

        // Encuentra el almacén seleccionado basado en el paquete
        var selectedWarehouse = warehouses.FirstOrDefault(w => w.Id == deliveryRequest.Package.WarehouseId);
        if (selectedWarehouse == null)
        {
            return NotFound("Almacén no encontrado");
        }

        // Simular datos aleatorios para el número de seguimiento, tiempo de entrega y precio
        Random random = new Random();
        string trackingNumber = GenerateTrackingNumber(random, 15);
        int deliveryTime = selectedWarehouse.Diasentregadefault + random.Next(1, 10); // Tiempo de entrega entre 1 y 10 días
        double shippingCost = Math.Round(random.NextDouble() * 150 + 50, 1); // Precio de envío aleatorio entre 50 y 200 con 1 solo decimal

        // Devolver los resultados
        return Ok(new
        {
            TrackingNumber = trackingNumber,
            DeliveryTime = deliveryTime,
            ShippingCost = shippingCost
        });
    }

    private string GenerateTrackingNumber(Random random, int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}

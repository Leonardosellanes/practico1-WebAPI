namespace Envios_Tiempo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TiemposEntrega;

    [ApiController]
    [Route("[controller]")]
    public class PackageController : ControllerBase
    {
        private List<Warehouse> warehouses = new List<Warehouse>
    {
        new Warehouse { Id = 1, Name = "Warehouse A", DefaultShippingTimeInDays = 2, Latitude = 40.7128, Longitude = -74.0060 }, // Example location
        new Warehouse { Id = 2, Name = "Warehouse B", DefaultShippingTimeInDays = 3, Latitude = 34.0522, Longitude = -118.2437 }, // Example location
        // Add more warehouses with default times and location information
    };

        [HttpPost]
        [Route("api/packages/calculate")]

        public IActionResult CalculateDeliveryTime([FromBody] DeliveryRequest deliveryRequest)
        {
            // Simulate a delay for the mock API
            Thread.Sleep(1000);

            // Find the selected warehouse based on the package
            var selectedWarehouse = warehouses.FirstOrDefault(w => w.Id == deliveryRequest.Package.WarehouseId);
            if (selectedWarehouse == null)
            {
                return NotFound("Warehouse not found");
            }

            // Calculate delivery time based on the distance to the user's location
            double distance = CalculateDistance(selectedWarehouse, deliveryRequest.UserLocation);

            // Calculate delivery time based on the distance and default shipping time
            int deliveryTime = selectedWarehouse.DefaultShippingTimeInDays + (int)(distance / 100); // Adjust the division factor based on your needs.

            return Ok(new { DeliveryTime = deliveryTime });
        }

        private double CalculateDistance(Warehouse warehouse, UserLocation userLocation)
        {
            // Replace this with a real distance calculation method (e.g., using the Haversine formula).
            // The example below is a simplified and non-accurate distance calculation.
            double latDiff = Math.Abs(warehouse.Latitude - userLocation.Latitude);
            double lonDiff = Math.Abs(warehouse.Longitude - userLocation.Longitude);
            return Math.Sqrt(latDiff * latDiff + lonDiff * lonDiff);
        }
    }

}

namespace Envios_Tiempo
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DefaultShippingTimeInDays { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}


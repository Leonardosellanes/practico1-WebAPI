namespace WebAPI.Models
{
    public class ApplicationUserUpdateModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? LName { get; set; }
        public bool? IsAdmin { get; set; }
        public string? Address { get; set; }
        public int ? EmpresaId { get; set; }

    }

}

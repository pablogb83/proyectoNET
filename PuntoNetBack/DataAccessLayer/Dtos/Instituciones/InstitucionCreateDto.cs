using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Dtos.Instituciones
{
    public class InstitucionCreateDto
    {
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Debe ingresar una direccion")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Debe ingresar un telefono")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Debe ingresar un producto")]
        public string PlanId { get; set; }
        public string ConnectionString { get; set; }
    }
}

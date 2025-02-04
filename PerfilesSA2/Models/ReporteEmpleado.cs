public class ReporteEmpleado
{
    public string Nombre { get; set; }
    public string DPI { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int Edad => DateTime.Now.Year - FechaNacimiento.Year - (DateTime.Now.DayOfYear < FechaNacimiento.DayOfYear ? 1 : 0);
    public DateTime FechaIngreso { get; set; }
    public string Genero { get; set; }
    public string NombreDepartamento { get; set; }
    public bool Estado { get; set; } 
}

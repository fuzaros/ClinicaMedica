using Microsoft.EntityFrameworkCore;

public class ClinicaMedicaContext(DbContextOptions<ClinicaMedicaContext> options) : DbContext(options)
{
    public DbSet<ClinicaMedica.Models.Paciente> Paciente { get; set; } = default!;
    public DbSet<ClinicaMedica.Models.Consulta> Consulta { get; set; } = default!;
}

using Microsoft.EntityFrameworkCore;

public class ClinicaMedicaContext(DbContextOptions<ClinicaMedicaContext> options) : DbContext(options)
{
    public DbSet<ClinicaMedica.Models.Paciente> Paciente { get; set; } = default!;
}

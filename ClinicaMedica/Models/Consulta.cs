using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedica.Models
{
    public class Consulta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O paciente é obrigatório.")]
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public virtual Paciente? Paciente { get; set; }

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [StringLength(50, ErrorMessage = "A especialidade não pode passar de 50 caracteres.")]
        [Display(Name = "Especialidade Médica")]
        public string Especialidade { get; set; }

        [Required(ErrorMessage = "O nome do médico é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do médico deve ter entre 2 e 100 caracteres.", MinimumLength = 2)]
        [Display(Name = "Médico")]
        public string NomeMedico { get; set; }

        [Required(ErrorMessage = "A data e horário da consulta são obrigatórios.")]
        [Display(Name = "Data e Horário")]
        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        [StringLength(500, ErrorMessage = "As observações devem ter no máximo 500 caracteres.")]
        [Display(Name = "Observações/Sintomas")]
        public string? Observacoes { get; set; }
    }
}

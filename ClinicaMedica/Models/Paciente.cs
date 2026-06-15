using System.ComponentModel.DataAnnotations;

namespace ClinicaMedica.Models
{
    public class Paciente {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do paciente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode passar de 100 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Formato de CPF inválido. Use 000.000.000-00")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
  
}

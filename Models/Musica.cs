using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMusicas.Models
{
    public class Musica
    {
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do aluno é obrigatório.")]
    [StringLength(100)]
    [Display(Name = "Nome do aluno")]
    public string NomeAluno { get; set; } = string.Empty;

    [Required(ErrorMessage = "O nome da música é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Nome da música")]
    public string NomeMusica { get; set; } = string.Empty;

    [Required(ErrorMessage = "O autor é obrigatório.")]
    [StringLength(150)]
    public string Autor { get; set; } = string.Empty;

    [Required(ErrorMessage = "O link do YouTube é obrigatório.")]
    [StringLength(500)]
    [Url(ErrorMessage = "Digite um link válido.")]
    [Display(Name = "Link do YouTube")]
    public string LinkYoutube { get; set; } = string.Empty;

    public DateTime DataCadastro { get; set; }
    }
}
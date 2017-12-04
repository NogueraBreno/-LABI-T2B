using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp_Breno.Models
{
    public class Musica
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }
        [Required (ErrorMessage = "Titulo Obrigatorio")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Tipo de Midia Obrigatorio")]
        [Display(Name = "Tipo de Midia")]
        public Tipo Tipo { get; set; }
        [Required(ErrorMessage = "Categoria Obrigatoria")]
        public Categoria Categoria { get; set; }
        [Display (Name = "Data de Cadastro")]
        public DateTime Data { get; set; }

    }
}
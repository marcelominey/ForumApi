using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class Usuario
    {
        [Key()]
        public int Idusuario { get; set; }

        [Display(Name="Nome do Usuário")]
        [Required(ErrorMessage="este campo não pode ficar vazio.")]
        [MinLength(2,ErrorMessage="Insira um nome com mais de 2 caracteres")]
        [MaxLength(10,ErrorMessage="Insira um nome com menos de 10 caracteres")]
        public string Nome { get; set; }
        
        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        public string Login { get; set; }

        [MinLength(8)]
        [MaxLength(12)]
        [RegularExpression(@"^[a-zA-Z-'\s]{1,40}$", ErrorMessage="Não é possível utilizar caracteres especiais")]
        public string Senha { get; set; }
        
        //[Range]
        public DateTime DataCadastro { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CalcPorPessoa
{
	public class Pessoa
	{
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		[Range(0, 100, ErrorMessage = "O campo {0} deve estar entre {2} e {1}.")]
		[Display(Name = "Número de dependentes")]
		public int NumDependentes { get; set; }
		public decimal ValorCalculado { get; set; }

		public Pessoa()
		{

		}

		public Pessoa(string nome, int numDependentes)
		{
			Nome = nome;
			NumDependentes = numDependentes;
		}
	}
}

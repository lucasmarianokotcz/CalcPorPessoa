using System;
using System.ComponentModel.DataAnnotations;

namespace CalcPorPessoa
{
	public class Pessoa
	{
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		[Range(1, 100, ErrorMessage = "O campo {0} deve estar entre {1} e {2}.")]
		[Display(Name = "N.º de dependentes")]
		public int NumDependentes { get; set; }

		[Display(Name = "Valor (R$)")]
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
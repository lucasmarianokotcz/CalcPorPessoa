using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CalcPorPessoa
{
	public class Pessoa
	{
		public string Nome { get; set; }
		[Display(Name = "Número de dependentes")]
		public int NumDependentes { get; set; }
		public decimal ValorCalculado { get; set; }

		public Pessoa(string nome, int numDependentes)
		{
			Nome = nome;
			NumDependentes = numDependentes;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace CalcPorPessoa
{
	public class Operacoes
	{
		private decimal _valorAPagar = 0;

		public List<Pessoa> ListaPessoas { get; set; }

		public Operacoes()
		{
			ListaPessoas = new List<Pessoa>();
		}

		private void CalcularValores(decimal valorAPagar)
		{
			_valorAPagar = valorAPagar;
			int totalDependentes = 0;
			foreach (Pessoa p in ListaPessoas)
			{
				totalDependentes += p.NumDependentes;
			}

			while (_valorAPagar * 100 % totalDependentes != 0)
			{
				_valorAPagar += 0.01m;
			}

			foreach (Pessoa p in ListaPessoas)
			{
				p.ValorCalculado = Math.Round(_valorAPagar / totalDependentes * p.NumDependentes, 2);
			}
		}

		public string ValoresPorPessoa(decimal valorAPagar)
		{
			CalcularValores(valorAPagar);

			string relatório = "Relatório de valores por pessoa:\n";
			decimal somaValores = 0;
			foreach (Pessoa p in ListaPessoas)
			{
				relatório += (p.Nome + ":     " + p.ValorCalculado.ToString("c2") + "\n");
				somaValores += p.ValorCalculado;
			}
			relatório += "\nValor da compra: " + valorAPagar.ToString("c2") + "\nSoma dos valores: " + somaValores.ToString("c2");
			return relatório;
		}

		public void AdicionarPessoa(string nome, int numDependentes)
		{
			if (!ListaPessoas.Any(p => p.Nome.ToLower() == nome.Trim().ToLower()))
			{
				Pessoa p = new Pessoa(nome.Trim(), numDependentes);
				ListaPessoas.Add(p);
			}
			else
			{
				throw new Exception("Já existe uma pessoa com esse nome!");
			}
		}

		public void EditarPessoa(string nomeAntigo, string nomeNovo, int numDependentes)
		{
			if ((!ListaPessoas.Any(p => p.Nome.ToLower() == nomeNovo.Trim().ToLower())) || nomeNovo == nomeAntigo)
			{
				ListaPessoas.RemoveAll(p => p.Nome.ToLower() == nomeAntigo.ToLower());
				Pessoa p = new Pessoa(nomeNovo.Trim(), numDependentes);
				ListaPessoas.Add(p);
			}
			else
			{
				throw new Exception("Já existe uma pessoa com esse nome!");
			}
		}

		public void ExcluirPessoa(string nome)
		{
			ListaPessoas.RemoveAll(p => p.Nome.ToLower() == nome.ToLower());
		}

		public void ExcluirTodasPessoas()
		{
			ListaPessoas.Clear();
		}
	}
}
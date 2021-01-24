using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CalcPorPessoa.Tests
{
	[TestClass]
	public class OperacoesTest
	{
		Operacoes op = new Operacoes();

		[TestMethod]
		public void AdicionarPessoa()
		{
			op.AdicionarPessoa("Lucas", 4);
			Assert.IsTrue(op.ListaPessoas.Count > 0);
		}

		[TestMethod]
		public void AdicionarPessoaDuplicada()
		{
			op.AdicionarPessoa("Lucas", 4);
			op.AdicionarPessoa("Lucas", 2);
			Assert.IsTrue(op.ListaPessoas.Count == 1);
		}

		[TestMethod]
		public void EditarPessoa()
		{
			op.AdicionarPessoa("Lucas", 4);
			op.AdicionarPessoa("Alex", 2);
			op.EditarPessoa("Alex", "BeiKeR", 10);
			Assert.IsTrue(op.ListaPessoas.Any(p => p.Nome == "BeiKeR"));
		}

		[TestMethod]
		public void EditarPessoaDuplicada()
		{
			op.AdicionarPessoa("Lucas", 4);
			op.AdicionarPessoa("Alex", 2);
			op.EditarPessoa("Alex", "Lucas", 10);
			Assert.IsTrue(op.ListaPessoas.Any(p => p.Nome == "Lucas"));
		}

		[TestMethod]
		public void ExcluirPessoa()
		{
			op.AdicionarPessoa("Lucas", 4);
			op.AdicionarPessoa("Alex", 2);
			op.ExcluirPessoa("Alex");
			Assert.IsTrue(op.ListaPessoas.Any(p => p.Nome == "Lucas") && op.ListaPessoas.Count == 1);
		}

		[TestMethod]
		public void ValoresPorPessoa()
		{
			op.AdicionarPessoa("Lucas", 4);
			op.AdicionarPessoa("Alex", 2);
			string str = op.ValoresPorPessoa(80.23m);
			Assert.IsTrue(str.Length > 0);
		}
	}
}

using DataAnnotationsExtensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalcPorPessoa.WebUI.ViewModel
{
	public class OperacoesViewModel
	{
		public Operacoes Operacoes { get; set; }
		public Pessoa Pessoa { get; set; }
		public bool OperationAddFailed { get; set; }
		public bool OperationEditFailed { get; set; }
		public bool OperationResultFailed { get; set; }
		public string NomeAntigo { get; set; }

		[RegularExpression(@"^([1-9]{1}[\d]{0,2}(\.[\d]{3})*(\,[\d]{0,2})?|[1-9]{1}[\d]{0,}(\,[\d]{0,2})?|0(\,[\d]{0,2})?|(\,[\d]{1,2})?)$", ErrorMessage = "O campo {0} deve ser um número válido.")]
		[Min(10.0, ErrorMessage = "O valor mínimo do campo {0} é {1}.")]
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		[Display(Name = "Valor da compra (R$)")]
		public decimal ValorCompra { get; set; } = 10;

		public RelatorioViewModel Relatorio { get; set; }
	}

	public class RelatorioViewModel
	{
		public List<Pessoa> LstPessoa { get; set; } = new List<Pessoa>();
		public decimal ValorCompra { get; set; }
		public decimal SomaValores { get; set; }
	}
}
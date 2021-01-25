using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CalcPorPessoa.WebUI.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		[BindProperty]
		public Operacoes Operacoes { get; set; }

		public void OnGet()
		{
			if (TempData["Operacoes"] != null)
			{
				Operacoes = JsonConvert.DeserializeObject<Operacoes>(TempData["Operacoes"].ToString());
			}
			else
			{
				Operacoes ??= new Operacoes();
				Operacoes.ListaPessoas.Add(new Pessoa("Lucas", 4));
				Operacoes.ListaPessoas.Add(new Pessoa("Alex", 2));
				Operacoes.ListaPessoas.Add(new Pessoa("Joel", 5));
				Operacoes.ListaPessoas.Add(new Pessoa("Teresa", 1));
			}

			TempData["Operacoes"] = JsonConvert.SerializeObject(Operacoes);
		}
	}
}
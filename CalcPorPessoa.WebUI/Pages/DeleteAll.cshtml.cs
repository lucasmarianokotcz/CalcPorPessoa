using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CalcPorPessoa.WebUI.Pages
{
	public class DeleteAllModel : PageModel
	{
		[BindProperty]
		public Operacoes Operacoes { get; set; }

		public IActionResult OnGet()
		{
			return RedirectToPage("./Index");
		}

		public IActionResult OnPost()
		{
			if (TempData["Operacoes"] == null)
			{
				return RedirectToPage("./Index");
			}

			Operacoes = JsonConvert.DeserializeObject<Operacoes>(TempData["Operacoes"].ToString());
			Operacoes.ExcluirTodasPessoas();

			TempData["Operacoes"] = JsonConvert.SerializeObject(Operacoes);

			return RedirectToPage("./Index");
		}
	}
}
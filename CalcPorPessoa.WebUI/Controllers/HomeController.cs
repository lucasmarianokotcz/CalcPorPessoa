using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalcPorPessoa.WebUI.Controllers
{
	public class HomeController : Controller
	{
		private static Operacoes operacoes = new Operacoes();

		// GET: HomeController
		public ActionResult Index()
		{
			return View(operacoes);
		}

		// GET: HomeController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: HomeController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: HomeController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: HomeController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// POST: HomeController/Delete/Lucas
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(string nome)
		{
			try
			{
				operacoes.ExcluirPessoa(nome);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// POST: HomeController/DeleteAll
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteAll()
		{
			try
			{
				operacoes.ExcluirTodasPessoas();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
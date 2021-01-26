using CalcPorPessoa.WebUI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CalcPorPessoa.WebUI.Controllers
{
	public class HomeController : Controller
	{
		#region Properties

		public OperacoesViewModel Operacoes => HttpContext.Session.GetObjectFromJson<OperacoesViewModel>("operacoes");

		#endregion Properties

		#region Http Methods

		public ActionResult Index()
		{
			SetOperacoesViewModelInitial();
			OperacoesViewModel op = Operacoes;

			if (TempData["IsRedirect"] != null && (bool)TempData["IsRedirect"])
			{
				return View(op);
			}

			op.OperationFailed = false;
			return View(op);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Nova(OperacoesViewModel opVM)
		{
			OperacoesViewModel op = Operacoes;

			if (ModelState.IsValid)
			{
				try
				{
					op.Operacoes.AdicionarPessoa(opVM.Pessoa.Nome, opVM.Pessoa.NumDependentes);
					op.OperationFailed = false;
					HttpContext.Session.SetObjectAsJson("operacoes", op);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					return CreateUserFailed(op, ex.Message);
				}
			}
			return CreateUserFailed(op, "Verifique campos inválidos.");
		}

		public ActionResult Edit(int id)
		{
			return View();
		}

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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(string nome)
		{
			OperacoesViewModel op = Operacoes;
			op.Operacoes.ExcluirPessoa(nome);
			HttpContext.Session.SetObjectAsJson("operacoes", op);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteAll()
		{
			OperacoesViewModel op = Operacoes;
			op.Operacoes.ExcluirTodasPessoas();
			HttpContext.Session.SetObjectAsJson("operacoes", op);
			return RedirectToAction(nameof(Index));
		}

		#endregion Http Methods

		#region Private Methods

		private void SetOperacoesViewModelInitial()
		{
			if (HttpContext.Session.GetObjectFromJson<OperacoesViewModel>("operacoes") == null)
			{
				HttpContext.Session.SetObjectAsJson("operacoes", new OperacoesViewModel() { Operacoes = new Operacoes() });
			}
		}

		private ActionResult CreateUserFailed(OperacoesViewModel op, string errorMessage)
		{
			TempData["IsRedirect"] = true;
			ModelState.AddModelError(string.Empty, errorMessage);
			op.OperationFailed = true;
			HttpContext.Session.SetObjectAsJson("operacoes", op);
			return View("Index", op);
		}

		#endregion Private Methods
	}
}
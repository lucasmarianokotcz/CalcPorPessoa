using CalcPorPessoa.WebUI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalcPorPessoa.WebUI.Controllers
{
	public class HomeController : Controller
	{
		#region Properties

		public OperacoesViewModel Operacoes => HttpContext.Session.GetObjectFromJson<OperacoesViewModel>("operacoes");

		#endregion Properties

		#region Http Methods

		public IActionResult Index()
		{
			SetOperacoesViewModelInitial();
			OperacoesViewModel op = Operacoes;

			string relatorio = string.Empty;

			if (TempData["Relatorio"] != null)
			{
				relatorio = TempData["Relatorio"] as string;
			}

			//ViewBag.Relatorio = FormatRelatorio(relatorio);
			op.Relatorio = FormatRelatorio(relatorio);

			if (TempData["IsRedirect"] != null && (bool)TempData["IsRedirect"])
			{
				return View(op);
			}

			op.OperationAddFailed = op.OperationEditFailed = op.OperationResultFailed = false;
			return View(op);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Nova(OperacoesViewModel opVM)
		{
			OperacoesViewModel op = Operacoes;

			if (ModelState.IsValid)
			{
				try
				{
					op.Operacoes.AdicionarPessoa(opVM.Pessoa.Nome, opVM.Pessoa.NumDependentes);
					return OperationSuccess(op);
				}
				catch (Exception ex)
				{
					return CreateUserFailed(op, ex.Message);
				}
			}
			return CreateUserFailed(op, "Verifique campos inválidos.");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Editar(OperacoesViewModel opVM)
		{
			OperacoesViewModel op = Operacoes;

			if (ModelState.IsValid)
			{
				try
				{
					op.Operacoes.EditarPessoa(opVM.NomeAntigo, opVM.Pessoa.Nome, opVM.Pessoa.NumDependentes);
					return OperationSuccess(op);
				}
				catch (Exception ex)
				{
					return EditUserFailed(op, ex.Message);
				}
			}
			return EditUserFailed(op, "Verifique campos inválidos.");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(string nome)
		{
			OperacoesViewModel op = Operacoes;
			op.Operacoes.ExcluirPessoa(nome);
			return OperationSuccess(op);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteAll()
		{
			OperacoesViewModel op = Operacoes;
			op.Operacoes.ExcluirTodasPessoas();
			return OperationSuccess(op);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult VerResultado(OperacoesViewModel opVM)
		{
			OperacoesViewModel op = Operacoes;

			if (ModelState.IsValid)
			{
				try
				{
					TempData["Relatorio"] = op.Operacoes.ValoresPorPessoa(opVM.ValorCompra);
					op.ValorCompra = opVM.ValorCompra;
					return OperationSuccess(op);
				}
				catch (Exception ex)
				{
					return ResultFailed(op, ex.Message);
				}
			}
			return ResultFailed(op, "Verifique campos inválidos.");
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

		private IActionResult OperationSuccess(OperacoesViewModel op)
		{
			try
			{
				op.OperationAddFailed = op.OperationEditFailed = op.OperationResultFailed = false;
				HttpContext.Session.SetObjectAsJson("operacoes", op);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				return RedirectToAction(nameof(Index));
			}
		}

		private IActionResult CreateUserFailed(OperacoesViewModel op, string errorMessage)
		{
			try
			{
				TempData["IsRedirect"] = true;
				ModelState.AddModelError("Nova", errorMessage);
				op.OperationAddFailed = true;
				op.OperationEditFailed = op.OperationResultFailed = false;
				HttpContext.Session.SetObjectAsJson("operacoes", op);
				return View("Index", op);
			}
			catch (Exception)
			{
				return View("Index");
			}
		}

		private IActionResult EditUserFailed(OperacoesViewModel op, string errorMessage)
		{
			try
			{
				TempData["IsRedirect"] = true;
				ModelState.AddModelError("Editar", errorMessage);
				op.OperationEditFailed = true;
				op.OperationAddFailed = op.OperationResultFailed = false;
				HttpContext.Session.SetObjectAsJson("operacoes", op);
				return View("Index", op);
			}
			catch (Exception)
			{
				return View("Index");
			}
		}

		private IActionResult ResultFailed(OperacoesViewModel op, string errorMessage)
		{
			try
			{
				TempData["IsRedirect"] = true;
				ModelState.AddModelError("", errorMessage);
				op.OperationResultFailed = true;
				op.OperationAddFailed = op.OperationEditFailed = false;
				HttpContext.Session.SetObjectAsJson("operacoes", op);
				return View("Index", op);
			}
			catch (Exception)
			{
				return View("Index");
			}
		}

		private RelatorioViewModel FormatRelatorio(string relatorio)
		{
			try
			{
				var rel1 = relatorio.Split("\n\n", StringSplitOptions.RemoveEmptyEntries).First().Split("\n", StringSplitOptions.RemoveEmptyEntries);
				var rel2 = relatorio.Split("\n\n", StringSplitOptions.RemoveEmptyEntries).Last().Split("\n", StringSplitOptions.RemoveEmptyEntries);

				var resultRel1 = rel1.Skip(1);

				var lstPessoas = new List<Pessoa>();
				foreach (var item in resultRel1)
				{
					var nome = item.Split(':').First();
					var valor = item.Split(':').Last().Trim().Remove(0, 2);

					lstPessoas.Add(new Pessoa()
					{
						Nome = nome,
						ValorCalculado = decimal.Parse(valor)
					});
				}

				var relatorioVM = new RelatorioViewModel()
				{
					LstPessoa = lstPessoas,
					ValorCompra = decimal.Parse(rel2.First().Split(':').Last().Trim().Remove(0, 2)),
					SomaValores = decimal.Parse(rel2.Last().Split(':').Last().Trim().Remove(0, 2))
				};

				//relatorio = relatorio.Replace("\n", "<br>");
				return relatorioVM;
			}
			catch (Exception)
			{
				return new RelatorioViewModel();
			}
		}

		#endregion Private Methods
	}
}
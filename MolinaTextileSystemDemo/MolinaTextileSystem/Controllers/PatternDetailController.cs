using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.CustomersOrdersDetails;
using MolinaTextileSystem.Repositories.PatternDetails;

namespace MolinaTextileSystem.Controllers
{
	public class PatternDetailController : Controller
	{
		public readonly IPatternDetailRepository _patternDetailRepository;

		private SelectList _rawMaterialList;
		private SelectList _patternList;

		public PatternDetailController(IPatternDetailRepository patternDetailRepository)
		{
			_patternDetailRepository = patternDetailRepository;

			_rawMaterialList = new SelectList(
				_patternDetailRepository.GetAllRawMaterials(),
				nameof(RawMaterialsModel.RawMaterialId),
				nameof(RawMaterialsModel.RawMaterialName)
			);
			_patternList = new SelectList(
				_patternDetailRepository.GetAllPatterns(),
				nameof(PatternModel.PatternId),
				nameof(PatternModel.PatternName)
			);
		}

		// GET: PatternDetailController
		public ActionResult Index()
		{
			return View(_patternDetailRepository.GetAll());
		}

		// GET: PatternDetailController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: PatternDetailController/Create
		public ActionResult Create()
		{
			ViewBag.RawMaterials = _rawMaterialList;
			ViewBag.Patterns = _patternList;

			return View();
		}

		// POST: PatternDetailController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(PatternDetailModel patternDetail)
		{
			try
			{
				_patternDetailRepository.Add(patternDetail);

				TempData["message"] = "Datos guardados correctamente.";

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["message"] = ex.Message;

				ViewBag.RawMaterials = _rawMaterialList;
				ViewBag.Patterns = _patternList;

				return View(patternDetail);
			}
		}

		// GET: PatternDetailController/Edit/5
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var patternDetail = _patternDetailRepository.GetById(id);

			if (patternDetail == null)
			{
				return NotFound();
			}

			_rawMaterialList = new SelectList(
				_patternDetailRepository.GetAllRawMaterials(),
				nameof(RawMaterialsModel.RawMaterialId),
				nameof(RawMaterialsModel.RawMaterialName),
				patternDetail?.RawMaterial?.RawMaterialId
			);
			_patternList = new SelectList(
				_patternDetailRepository.GetAllPatterns(),
				nameof(PatternModel.PatternId),
				nameof(PatternModel.PatternName),
				patternDetail?.Pattern?.PatternId
			);

			ViewBag.RawMaterials = _rawMaterialList;
			ViewBag.Patterns = _patternList;

			return View(patternDetail);
		}

		// POST: PatternDetailController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(PatternDetailModel patternDetail)
		{
			try
			{
				_patternDetailRepository.Edit(patternDetail);

				TempData["message"] = "Datos editados correctamente.";

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["message"] = ex.Message;

				ViewBag.RawMaterials = _rawMaterialList;
				ViewBag.Patterns = _patternList;

				return View(patternDetail);
			}
		}

		// GET: PatternDetailController/Delete/5
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var patternDetail = _patternDetailRepository.GetById(id);

			if (patternDetail == null)
			{
				return NotFound();
			}

			return View(patternDetail);
		}

		// POST: PatternDetailController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(PatternDetailModel patternDetail)
		{
			try
			{
				_patternDetailRepository.Delete(patternDetail.PatternDetailId);

				TempData["message"] = "Dato eliminado correctamente.";

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["message"] = ex.Message;

				return View(patternDetail);
			}
		}
	}
}

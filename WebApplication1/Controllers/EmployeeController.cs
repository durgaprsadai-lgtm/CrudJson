using Microsoft.AspNetCore.Mvc;
using InMemoryCrudApp.Models;
using InMemoryCrudApp.Services;

namespace InMemoryCrudApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        public IActionResult Index() => View(_service.GetAll());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Employee employee, string plainPassword)
        {
            _service.Add(employee, plainPassword);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var emp = _service.GetById(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee, string plainPassword)
        {
            _service.Update(employee, plainPassword);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var emp = _service.GetById(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var emp = _service.GetById(id);
            return View(emp);
        }
    }
}

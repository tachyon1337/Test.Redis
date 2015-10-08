using System;
using System.Linq;
using System.Web.Mvc;
using RevStack.Pattern;
using Test.Redis.Models;

namespace Test.Redis.Controllers
{
    public class HomeController : Controller
    {
        private IService<SampleModel, Guid> _service;
        private IUnitOfWork<SampleModel, Guid> _unitOfWork;
        public HomeController(IService<SampleModel,Guid> service, IUnitOfWork<SampleModel,Guid> unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TestSample()
        {
            var sampleModel = new SampleModel
            {
                Name = "Bob Jackson",
                CurrentDate = DateTime.Now
            };

            _service.Add(sampleModel);
            _unitOfWork.Commit();

            return View();
        }

        public ActionResult ViewSample()
        {
            var result = _service.Get();
            return View(result);
        }

        public ActionResult EditSample(Guid id)
        {
            var sampleModel = _service.Find(x => x.Id == id).SingleOrDefault();
            sampleModel.Name = "Edited Name: " + sampleModel.Name;
            _service.Update(sampleModel);
            _unitOfWork.Commit();

            return View();
        }

        public ActionResult DeleteSample(Guid id)
        {
            var sampleModel = _service.Find(x => x.Id == id).SingleOrDefault();
            _service.Delete(sampleModel);
            _unitOfWork.Commit();

            return View();
        }

        public ActionResult QueueSample()
        {
            var sampleModel = new SampleModel
            {
                Name = "Jim Carey",
                CurrentDate = DateTime.Now
            };
            _service.Add(sampleModel);

            return View();
        }
    }
}
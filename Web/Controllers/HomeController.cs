using System;
using System.Globalization;
using System.Web.Mvc;
using AnsiSoft.Calculator.Model.Interface.Facade;

namespace AnsiSoft.Calculator.Web.Controllers
{
    /// <summary>
    /// Clas-contoller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Calculator instance
        /// </summary>
        public IProcessor Processor { get; }
        /// <summary>
        /// Create new <see cref="HomeController" /> instance.
        /// </summary>
        /// <param name="processorFactory">factory for create processor class.</param>
        public HomeController(IProcessorFactory processorFactory)
        {
            Processor = processorFactory.CreateProcessor();
        }

        /// <summary>
        /// Home Page
        /// </summary>
        /// <returns>Home Page View</returns>
        public ActionResult Index() => View();

        /// <summary>
        /// About Page
        /// </summary>
        /// <returns>About Page View</returns>
        public ActionResult About() => View();

        /// <summary>
        /// Contact Page
        /// </summary>
        /// <returns>Contact Page View</returns>
        public ActionResult Contact() => View();

        /// <summary>
        /// Calculate text expression.
        /// </summary>
        /// <param name="text">Input text expression</param>
        /// <returns>Result or error message</returns>
        public ActionResult Calculate(string text)
        {
            string result;
            try
            {
                var value = Processor.Calculate(text);
                result = value.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception exception)
            {
                result = exception.Message;
            }
            return Json(new {result}, JsonRequestBehavior.AllowGet);
        }
    }
}
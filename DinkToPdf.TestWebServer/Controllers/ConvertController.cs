using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DinkToPdf.Contracts;
using System.IO;
using Microsoft.IO;

namespace DinkToPdf.TestWebServer.Controllers
{
    [Route("api/[controller]")]
    public class ConvertController : Controller
    {
        private IConverter _converter;

        public ConvertController(IConverter converter)
        {
            _converter = converter;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A3,
                    Orientation = Orientation.Landscape,
                },

                Objects = {
                    new ObjectSettings()
                    {
                        Page = "http://google.com/",
                    },
                     new ObjectSettings()
                    {
                        Page = "https://github.com/",
                         
                    }
                }
            };
           
            var pdf = _converter.Convert(doc) as RecyclableMemoryStream;


            return new FileContentResult(pdf.GetBuffer(), "application/pdf");
        }
    }
}

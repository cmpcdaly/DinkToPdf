using Microsoft.IO;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DinkToPdf.TestThreadSafe
{
    public class Program
    {
        static SynchronizedConverter converter;

        public static void Main(string[] args)
        {
            converter = new SynchronizedConverter(new PdfTools(new RecyclableMemoryStreamManager()));

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Grayscale,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings() { Top = 10 },
                },
                Objects = {
                    new ObjectSettings() {
                        Page = "http://www.color-hex.com/"
                    }
                }
            };

            Task.Run(() => Action(doc));

            var doc2 = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4Small
                },

                Objects = {
                    new ObjectSettings()
                    {
                        Page = "http://google.com/",
                    }
                }
            };


            Task.Run(() => Action(doc2));

            Console.ReadKey();
        }

        private static void Action(HtmlToPdfDocument doc)
        {
            if (!Directory.Exists("Files"))
            {
                Directory.CreateDirectory("Files");
            }

            using (var stream = new FileStream(@"Files\" + DateTime.UtcNow.Ticks + ".pdf", FileMode.Create))
            using (var pdfStream = converter.Convert(doc))
            {
                pdfStream.CopyTo(stream);
            }
        }
    }
}

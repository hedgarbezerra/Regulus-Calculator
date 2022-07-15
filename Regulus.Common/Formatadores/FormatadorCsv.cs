using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using CsvHelper;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Regulus.Common.Formatadores
{
    public class FormatadorCsv : TextOutputFormatter
    {
        public FormatadorCsv()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {

            var httpContext = context.HttpContext;
            string result;

            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                if (!typeof(IEnumerable<dynamic>).IsAssignableFrom(context.ObjectType))
                {
                    var listaObjetos = new List<object>(1)
                        {
                            context.Object
                        };

                    csv.WriteRecords(listaObjetos);
                }
                else
                    csv.WriteRecords((IEnumerable<dynamic>)context.Object);

                result = writer.ToString();
            }
            await httpContext.Response.WriteAsync(result, selectedEncoding);
        }
    }
}

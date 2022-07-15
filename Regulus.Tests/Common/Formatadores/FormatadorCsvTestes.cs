using Microsoft.AspNetCore.Mvc.Formatters;
using Regulus.Common.Formatadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regulus.Tests.Common.Formatadores
{
    [TestFixture]
    public class FormatadorCsvTestes
    {
        private MockRepository _mockRepository;



        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private FormatadorCsv CreateFormatadorCsv()
        {
            return new FormatadorCsv();
        }

        [Test]
        public async Task WriteResponseBodyAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var formatadorCsv = this.CreateFormatadorCsv();
            OutputFormatterWriteContext context = null;
            Encoding selectedEncoding = Encoding.UTF8;

            // Act
            await formatadorCsv.WriteResponseBodyAsync(context, selectedEncoding);

            // Assert
            Assert.Fail();
        }
    }

}
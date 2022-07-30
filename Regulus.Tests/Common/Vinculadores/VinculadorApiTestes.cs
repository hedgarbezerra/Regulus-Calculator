using Microsoft.Extensions.DependencyInjection;
using Regulus.Common.Vinculadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regulus.Tests.Common.Vinculadores
{
    [TestFixture]
    public class VinculadorApiTestes
    {
        private MockRepository _mockRepository;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private VinculadorApi CreateVinculadorApi()
        {
            return new VinculadorApi();
        }

        [Test]
        public void Vincular_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var vinculadorApi = this.CreateVinculadorApi();

            // Act
            vinculadorApi.Invoking(v => v.Vincular(null))
                .Should()
                .Throw<ArgumentNullException>();
        }
    }
}
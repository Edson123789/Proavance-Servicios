using BSI.Integra.Test.PruebasUnitarias.Controlador;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Test.RepositorioMock
{
    public class OrigenRepositorioMock
    {
        public Mock<Origen> _origenRepositorio;

        public OrigenRepositorioMock()
        {
            _origenRepositorio = new Mock<Origen>();
        }

        private void SetUp()
        {
            _origenRepositorio.Setup((x) => x.Delete(It.Is<int>(p => p.Equals(3)), It.IsAny<string>())).Returns(value: true);
        }
    }
}

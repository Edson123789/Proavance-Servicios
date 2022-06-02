using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EstructuraEspecificaCapituloBO : BaseBO
    {
        public int IdEstructuraEspecifica { get; set; }
        public int Numero { get; set; }
        public string Capitulo { get; set; }

        public int Insertar()
        {
            EstructuraEspecificaCapituloRepositorio _repEstructuraEspecifica = new EstructuraEspecificaCapituloRepositorio();
            _repEstructuraEspecifica.Insert(this);
            return this.Id;
        }

        public int Actualizar()
        {
            EstructuraEspecificaCapituloRepositorio _repEstructuraEspecifica = new EstructuraEspecificaCapituloRepositorio();
            _repEstructuraEspecifica.Update(this);
            return this.Id;
        }

        public int Eliminar(string usuario)
        {
            EstructuraEspecificaCapituloRepositorio _repEstructuraEspecifica = new EstructuraEspecificaCapituloRepositorio();
            _repEstructuraEspecifica.Delete(this.Id, usuario);
            return this.Id;
        }
    }
}

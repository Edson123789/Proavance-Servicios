using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EstructuraEspecificaSubSesionBO : BaseBO
    {
        public int IdEstructuraEspecificaSesion { get; set; }
        public int Numero { get; set; }
        public string SubSesion { get; set; }
        public int IdEstructuraEspecifica { get; set; }
        public int Insertar()
        {
            EstructuraEspecificaSubSesionRepositorio _repEstructuraEspecifica = new EstructuraEspecificaSubSesionRepositorio();
            _repEstructuraEspecifica.Insert(this);
            return this.Id;
        }

        public int Actualizar()
        {
            EstructuraEspecificaSubSesionRepositorio _repEstructuraEspecifica = new EstructuraEspecificaSubSesionRepositorio();
            _repEstructuraEspecifica.Update(this);
            return this.Id;
        }

        public int Eliminar(string usuario)
        {
            EstructuraEspecificaSubSesionRepositorio _repEstructuraEspecifica = new EstructuraEspecificaSubSesionRepositorio();
            _repEstructuraEspecifica.Delete(this.Id, usuario);
            return this.Id;
        }
    }
}

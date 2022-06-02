using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EstructuraEspecificaSesionBO : BaseBO
    {
        public int IdEstructuraEspecificaCapitulo { get; set; }
        public int Numero { get; set; }
        public string Sesion { get; set; }
        public int IdEstructuraEspecifica { get; set; }
        public int Insertar()
        {
            EstructuraEspecificaSesionRepositorio _repEstructuraEspecifica = new EstructuraEspecificaSesionRepositorio();
            _repEstructuraEspecifica.Insert(this);
            return this.Id;
        }

        public int Actualizar()
        {
            EstructuraEspecificaSesionRepositorio _repEstructuraEspecifica = new EstructuraEspecificaSesionRepositorio();
            _repEstructuraEspecifica.Update(this);
            return this.Id;
        }

        public int Eliminar(string usuario)
        {
            EstructuraEspecificaSesionRepositorio _repEstructuraEspecifica = new EstructuraEspecificaSesionRepositorio();
            _repEstructuraEspecifica.Delete(this.Id, usuario);
            return this.Id;
        }
    }
}

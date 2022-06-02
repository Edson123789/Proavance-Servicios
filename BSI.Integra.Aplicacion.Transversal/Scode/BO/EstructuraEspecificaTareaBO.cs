using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EstructuraEspecificaTareaBO : BaseBO
    {
        public int IdEstructuraEspecifica { get; set; }
        public int IdTarea { get; set; }
        public string NombreTarea { get; set; }
        public int OrdenCapitulo { get; set; }
        public int IdDocumentoSeccionPw { get; set; }

        public int Insertar()
        {
            EstructuraEspecificaTareaRepositorio _repEstructuraEspecifica = new EstructuraEspecificaTareaRepositorio();
            _repEstructuraEspecifica.Insert(this);
            return this.Id;
        }

        public int Actualizar()
        {
            EstructuraEspecificaTareaRepositorio _repEstructuraEspecifica = new EstructuraEspecificaTareaRepositorio();
            _repEstructuraEspecifica.Update(this);
            return this.Id;
        }

        public int Eliminar(string usuario)
        {
            EstructuraEspecificaTareaRepositorio _repEstructuraEspecifica = new EstructuraEspecificaTareaRepositorio();
            _repEstructuraEspecifica.Delete(this.Id, usuario);
            return this.Id;
        }

    }
}

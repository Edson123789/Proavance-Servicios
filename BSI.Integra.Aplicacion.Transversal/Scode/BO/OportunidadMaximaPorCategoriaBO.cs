using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class OportunidadMaximaPorCategoriaBO : BaseBO
    {
       
        public int IdPersonal {
            get { return idPersonal; }
            set
            {
                ValidarValorMayorCeroProperty(this.GetType().Name, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdPersonal").Name,
                                                  "Identificador del Asesor", value, 0);
                idPersonal = value;
            }
        }
        public int IdTipoCategoriaOrigen
        {
            get { return idTipoCategoriaOrigen; }
            set
            {
                ValidarValorMayorCeroProperty(this.GetType().Name, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdTipoCategoriaOrigen").Name,
                                                   "Identificador Categoria Origen", value);
                idTipoCategoriaOrigen = value;
            }
        }
        public int IdPais { get; set; }
        public int OportunidadesMaximas { get; set; }
        public int OportunidadesSinGenerarIs { get; set; }
        public int Meta { get; set; }
        public string Grupo { get; set; }

        //otros Atributos
        public int estadoPantalla2
        {
            get { return _estadoPantalla2; }
            set
            {
                ValidarRangoProperty(this.GetType().Name, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("estadoPantalla2").Name,
                                                  "Identificador Estado Pantalla", value, 3, -1);
                _estadoPantalla2 = value;
            }
        }
        //atributos privados
        private int idPersonal;
        private int idTipoCategoriaOrigen;
        private int _estadoPantalla2;


        public OportunidadMaximaPorCategoriaBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}

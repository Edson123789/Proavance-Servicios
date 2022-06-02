using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class RankingIngresoBO : BaseBO
    {
        public int? Inscritos { get; set; }
        public int? Cerradas { get; set; }
        public decimal? Tcreal { get; set; }
        public decimal? Tcmeta { get; set; }
        public decimal TcrealByMeta { get; set; }
        public double? IngresoOc { get; set; }
        public double? IngresoIs { get; set; }
        public double? IngresoMeta { get; set; }
        public double? IngresoReal { get; set; }
        public double? IrbyIm { get; set; }
        public int? RankingGeneral { get; set; }
        public double? IngresoPromedioOc { get; set; }
        public int? IdCategoriaAsesor { get; set; }
        public int? RankingTipoAsesor { get; set; }
        public int? IdPersonal {
            get { return _idPersonal; }
            set
            {
                ValidarValorMayorCeroProperty(this.GetType().Name, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdPersonal").Name,
                                                  "Identificador del Asesor", value ?? 0);
                _idPersonal = value ;
            }
        }
        private int? _idPersonal;

        public RankingIngresoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

    }
}

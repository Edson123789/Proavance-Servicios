using System;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.Builder
{
    public class BuilderOrquestaSentinel_SDT_BSGrupo_RepSBSItemDTO
    {
        public static SentinelSdtRepSBSItemDTO builderEntityDTO(SentinelService.SDT_IC_RepSBSSDT_IC_RepSBSItem entity)
        {
            SentinelSdtRepSBSItemDTO rpta = new SentinelSdtRepSBSItemDTO();

            rpta.TipoDoc = entity.TipoDocCPT;
            rpta.NroDoc = entity.NroDocCPT;
            rpta.NombreRazonSocial = entity.NomRazSocEnt;
            rpta.Calificacion = entity.Calificacion;
            rpta.MontoDeuda = Convert.ToDecimal(entity.MontoDeuda);
            rpta.DiasVencidos = entity.DiasVencidos;
            rpta.FechaReporte = entity.FechaReporte;
            return rpta;
        }

        public static IList<SentinelSdtRepSBSItemDTO> builderListEntityDTO(IEnumerable<SentinelService.SDT_IC_RepSBSSDT_IC_RepSBSItem> listaInput)
        {
            var listOutput = new List<SentinelSdtRepSBSItemDTO>();
            foreach (SentinelService.SDT_IC_RepSBSSDT_IC_RepSBSItem entityPO in listaInput)
            {
                listOutput.Add(builderEntityDTO(entityPO));
            }
            return listOutput;
        }
    }
}

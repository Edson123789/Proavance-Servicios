﻿using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Servicios.DTOs;

namespace BSI.Integra.Aplicacion.Servicios.Builder
{
    public class BuilderOrquestaSentinel_SDT_BSGrupo_RepLegItemDTO
    {
        public static SentinelRepLegItemDTO builderEntityDTO(SentinelService.SDT_IC_RepLegSDT_IC_RepLegItem entity)
        {
            SentinelRepLegItemDTO rpta = new SentinelRepLegItemDTO();

            rpta.TipoDocumento = entity.NMDocRLID;
            rpta.NumeroDocumento = entity.ETNumDocRL;
            rpta.Nombres = entity.ETNombreRL;
            rpta.ApellidoPaterno = entity.ETApePatRL;
            rpta.ApellidoMaterno = entity.ETApeMatRL;
            rpta.RazonSocial = entity.ETNomRL;
            rpta.Cargo = entity.ETCargoRL;
            rpta.SemaforoActual = entity.ETSemActRL;

            return rpta;

        }

        public static IList<SentinelRepLegItemDTO> builderListEntityDTO(IEnumerable<SentinelService.SDT_IC_RepLegSDT_IC_RepLegItem> listaInput)
        {
            var listOutput = new List<SentinelRepLegItemDTO>();
            foreach (SentinelService.SDT_IC_RepLegSDT_IC_RepLegItem entityPO in listaInput)
            {
                listOutput.Add(builderEntityDTO(entityPO));
            }
            return listOutput;
        }
    }
}

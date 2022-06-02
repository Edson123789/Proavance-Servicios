using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Persistencia.SCode.Repository;

namespace BSI.Integra.Aplicacion.Planificacion.SCode
{
    public class AreaRepositorio : BaseRepository<TArea, AreaBO>
    {
        public bool Insert(AreaBO entity)
        {
            try
            {
                //TArea entityDB = Mapper.Map<AreaBO, TArea>(entity);
                TArea entityDB = new TArea()
                {
                    Nombre = entity.Nombre,
                    Estado = entity.Estado,
                    UsuarioCreacion = entity.UsuarioCreacion,
                    UsuarioModificacion = entity.UsuarioModificacion,
                    FechaCreacion = entity.FechaCreacion,
                    FechaModificacion = entity.FechaModificacion
                };
                return base.Insert(entityDB);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

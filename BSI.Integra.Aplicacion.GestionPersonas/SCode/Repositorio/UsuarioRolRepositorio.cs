using AutoMapper;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Repositorio
{
     public class UsuarioRolRepositorio 
        //: BaseRepository<TUsuarioRol, UsuarioRolBO>
    {
        //#region Metodos Base
        //public UsuarioRolRepositorio() : base()
        //{
        //}
        //public UsuarioRolRepositorio(integraDBContext contexto) : base(contexto)
        //{
        //}
        //public IEnumerable<UsuarioRolBO> GetBy(Expression<Func<TUsuarioRol, bool>> filter)
        //{
        //    IEnumerable<TUsuarioRol> listado = base.GetBy(filter);
        //    List<UsuarioRolBO> listadoBO = new List<UsuarioRolBO>();
        //    foreach (var itemEntidad in listado)
        //    {
        //        UsuarioRolBO objetoBO = Mapper.Map<TUsuarioRol, UsuarioRolBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
        //        listadoBO.Add(objetoBO);
        //    }

        //    return listadoBO;
        //}
        //public UsuarioRolBO FirstById(int id)
        //{
        //    try
        //    {
        //        TUsuarioRol entidad = base.FirstById(id);
        //        UsuarioRolBO objetoBO = new UsuarioRolBO();
        //        Mapper.Map<TUsuarioRol, UsuarioRolBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

        //        return objetoBO;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        //public UsuarioRolBO FirstBy(Expression<Func<TUsuarioRol, bool>> filter)
        //{
        //    try
        //    {
        //        TUsuarioRol entidad = base.FirstBy(filter);
        //        UsuarioRolBO objetoBO = Mapper.Map<TUsuarioRol, UsuarioRolBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

        //        return objetoBO;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        //public bool Insert(UsuarioRolBO objetoBO)
        //{
        //    try
        //    {
        //        //mapeo de la entidad
        //        TUsuarioRol entidad = MapeoEntidad(objetoBO);

        //        bool resultado = base.Insert(entidad);
        //        if (resultado)
        //            AsignacionId(entidad, objetoBO);

        //        return resultado;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        //public bool Insert(IEnumerable<UsuarioRolBO> listadoBO)
        //{
        //    try
        //    {
        //        foreach (var objetoBO in listadoBO)
        //        {
        //            bool resultado = Insert(objetoBO);
        //            if (resultado == false)
        //                return false;
        //        }

        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public bool Update(UsuarioRolBO objetoBO)
        //{
        //    try
        //    {
        //        if (objetoBO == null)
        //        {
        //            throw new ArgumentNullException("Entidad nula");
        //        }

        //        //mapeo de la entidad
        //        TUsuarioRol entidad = MapeoEntidad(objetoBO);

        //        bool resultado = base.Update(entidad);
        //        if (resultado)
        //            AsignacionId(entidad, objetoBO);

        //        return resultado;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public bool Update(IEnumerable<UsuarioRolBO> listadoBO)
        //{
        //    try
        //    {
        //        foreach (var objetoBO in listadoBO)
        //        {
        //            bool resultado = Update(objetoBO);
        //            if (resultado == false)
        //                return false;
        //        }

        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        //private void AsignacionId(TUsuarioRol entidad, UsuarioRolBO objetoBO)
        //{
        //    try
        //    {
        //        if (entidad != null && objetoBO != null)
        //        {
        //            objetoBO.Id = entidad.Id;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}


        ////CORREGIR IDMIGRACION
        //private TUsuarioRol MapeoEntidad(UsuarioRolBO objetoBO)
        //{
        //    try
        //    {
        //        //crea la entidad padre
        //        TUsuarioRol entidad = new TUsuarioRol();
        //        entidad = Mapper.Map<UsuarioRolBO, TUsuarioRol>(objetoBO,
        //            opt => opt.ConfigureMap(MemberList.None).ForMember(dest => dest.Id, m => m.Ignore()));

        //        //mapea los hijos

        //        return entidad;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        //public IEnumerable<UsuarioRolBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TUsuarioRol, bool>>> filters, Expression<Func<TUsuarioRol, KProperty>> orderBy, bool ascending)
        //{
        //    IEnumerable<TUsuarioRol> listado = base.GetFiltered(filters, orderBy, ascending);
        //    List<UsuarioRolBO> listadoBO = new List<UsuarioRolBO>();

        //    foreach (var itemEntidad in listado)
        //    {
        //        UsuarioRolBO objetoBO = Mapper.Map<TUsuarioRol, UsuarioRolBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
        //        listadoBO.Add(objetoBO);
        //    }
        //    return listadoBO;
        //}
        //#endregion
    }
}

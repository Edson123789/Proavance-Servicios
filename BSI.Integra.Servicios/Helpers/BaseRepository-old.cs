using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Persistencia.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Helpers
{

    //public interface IOurTemplate<T, U>
    //    where T : class
    //    where U : class
    //{
    //    IQueryable<T> List();
    //    T Get(U id);
    //}

    //public interface IIntegraRepository<TEntidad>
    //    where TEntidad : BaseEntity
    //{
    //    bool ExisteObjeto(int id);
    //    TEntidad GetObjetoXID(int id);
    //    IQueryable<TEntidad> GetTabla();
    //    bool InsertObjeto(TEntidad entity);
    //    string EliminarObjeto(int id);
    //    bool ActualizaObjeto(TEntidad entity);

    //}

    //public class IntegraRepository<TEntidad> : IIntegraRepository<TEntidad>
    //    where TEntidad : BaseEntity
    //{
    //    public integraDBContext ObjectContext;
    //    public DbSet<TEntidad> entities;

    //    public IntegraRepository(integraDBContext objectContext)
    //    {
    //        ObjectContext = objectContext;
    //        entities = ObjectContext.Set<TEntidad>();
    //    }

    //    public bool ActualizaObjeto(TEntidad entity)
    //    {

    //        try
    //        {

    //            if (entity == null)
    //            {
    //                throw new ArgumentNullException("entity null");
    //            }
    //            ObjectContext.Update<TEntidad>(entity);
    //            return ObjectContext.SaveChanges() >= 0;
    //            //return true;
    //        }
    //        catch (Exception exception)
    //        {

    //            throw exception;
    //        }
    //    }

    //    public string EliminarObjeto(int id)
    //    {
    //        var objeto = entities.FirstOrDefault(w => w.Id == id && w.Estado.Value);
    //        objeto.Estado = false;
    //        //if (ObjectContext.SaveChanges() >= 0)
    //        //{
    //        //    return objeto.GetType().Name;
    //        //}
    //        //else
    //        //{
    //        //    return string.Empty;
    //        //}
    //        return null;
    //    }

    //    public bool ExisteObjeto(int id)
    //    {
    //        return entities.Where(w => w.Id == id).Any();

    //    }

    //    public TEntidad GetObjetoXID(int id)
    //    {
    //        return entities.FirstOrDefault(w => w.Id == id && w.Estado.Value);
    //    }

    //    public IQueryable<TEntidad> GetTabla()
    //    {

    //        return entities.AsQueryable();
    //    }

    //    public bool InsertObjeto(TEntidad entity)
    //    {
    //        try
    //        {

    //            if (entity == null)
    //            {
    //                throw new ArgumentNullException("entity null");
    //            }
    //            ObjectContext.Add<TEntidad>(entity);
    //            return ObjectContext.SaveChanges() >= 0;
    //            //return true;
    //        }
    //        catch (Exception exception)
    //        {

    //            throw exception;
    //        }
    //    }

    //}

}

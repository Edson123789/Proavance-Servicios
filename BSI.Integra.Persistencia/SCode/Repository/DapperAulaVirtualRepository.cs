using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BSI.Integra.Persistencia.Models.AulaVirtual;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BSI.Integra.Persistencia.SCode.Repository
{
    public class DapperAulaVirtualRepository
    {
        private int _timeout = 10 * 60;
        private AulaVirtualContext _context;

        public DapperAulaVirtualRepository()
        {
            _context = new AulaVirtualContext();
        }

        public DapperAulaVirtualRepository(AulaVirtualContext context)
        {
            _context = context;
        }

        //public string QueryDapper(string sql)
        //{
        //    IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql, CommandType.Text).ToList();

        //    var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
        //    return jsonResultado;
        //}

        public string QueryDapper(string sql, object parametros)
        {
            IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.Text, commandTimeout: _timeout).ToList();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        //public string FirstOrDefault(string sql)
        //{
        //    dynamic result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql, CommandType.Text).FirstOrDefault();

        //    var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
        //    return jsonResultado;
        //}

        public string FirstOrDefault(string sql, object parametros)
        {
            dynamic result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql, param: parametros,
                commandType: CommandType.Text, commandTimeout: _timeout).FirstOrDefault();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string QuerySPDapper(string sql, object parametros)
        {
            IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: _timeout).ToList();

            //IList<dynamic> result = _context.Database.GetDbConnection()
            //    .Query<dynamic>(sql, parametros, commandType: CommandType.StoredProcedure).ToList();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string QuerySPFirstOrDefault(string sql, object parametros)
        {
            dynamic result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: _timeout).FirstOrDefault();

            //IList<dynamic> result = _context.Database.GetDbConnection()
            //    .Query<dynamic>(sql, parametros, commandType: CommandType.StoredProcedure).ToList();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }
    }
}

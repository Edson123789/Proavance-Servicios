using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BSI.Integra.Aplicacion.Base.BO
{
    public static class ExtendedLinq
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static List<List<T>> Split<T>(this IList<T> source, int chunksize)
        {
            return source
               .Select((x, i) => new { Index = i, Value = x })
               .GroupBy(x => x.Index / chunksize)
               .Select(x => x.Select(v => v.Value).ToList())
               .ToList();
        }
        /// <summary>
        /// Separa una lista en grupos 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <param name="maxItems">Cantidad de items permitidos en un grupo</param>
        /// <returns></returns>
        public static IEnumerable<ICollection<T>> Split<T>(this IEnumerable<T> src, int maxItems)
        {
            var list = new List<T>();
            foreach (var t in src)
            {
                list.Add(t);
                if (list.Count == maxItems)
                {
                    yield return list;
                    list = new List<T>();
                }
            }
            if (list.Count > 0)
                yield return list;
        }

        /// <summary>
        /// Get properties form an object
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static IEnumerable<(string Name, object Value)> GetProperties(this object src)
        {
            if (src is IDictionary<string, object> dictionary)
            {
                return dictionary.Select(x => (x.Key, x.Value));
            }
            return src.GetObjectProperties().Select(x => (x.Name, x.GetValue(src)));
        }

        /// <summary>
        /// Get object properties
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetObjectProperties(this object src)
        {
            return src.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !p.GetGetMethod().GetParameters().Any());
        }

        private static Random _rnd = new Random();

        private static T GetAndRemoveRandomTeam<T>(this List<T> allTeams)
        {
            int randomIndex = _rnd.Next(allTeams.Count);
            T randomTeam = allTeams[randomIndex];
            allTeams.RemoveAt(randomIndex);
            return randomTeam;
        }

        public static List<List<T>> GenerateGroups<T>(this List<T> teams, int amount)
        {
            int teamCount = (int)teams.Count / amount;
            List<T> allteams = teams.ToList(); // copy to be able to remove items

            if (teamCount == 0)
                return new List<List<T>> { allteams };

            List<List<T>> allTeamGroups = new List<List<T>>();
            List<T> thisTeam = new List<T>();
            while (allteams.Count > 0)
            {
                if (thisTeam.Count == amount)
                {
                    allTeamGroups.Add(thisTeam);
                    thisTeam = new List<T>();
                }
                thisTeam.Add(GetAndRemoveRandomTeam(allteams));
            }
            allTeamGroups.Add(thisTeam);

            return allTeamGroups;
        }


        //--
        public static string ToListString(this IList<int> datos)
        {
            if (datos == null)
                datos = new List<int>();
            int NumberElements = datos.Count;
            string rptaCadena = string.Empty;
            for (int i = 0; i < NumberElements - 1; i++)
                rptaCadena += Convert.ToString(datos[i]) + ",";
            if (NumberElements > 0)
                rptaCadena += Convert.ToString(datos[NumberElements - 1]);
            return rptaCadena;
        }
        public static string ToListString(this IList<string> datos)
        {
            if (datos == null)
                datos = new List<string>();
            int NumberElements = datos.Count;
            string rptaCadena = string.Empty;
            for (int i = 0; i < NumberElements - 1; i++)
                rptaCadena += Convert.ToString(datos[i]) + ",";
            if (NumberElements > 0)
                rptaCadena += Convert.ToString(datos[NumberElements - 1]);
            return rptaCadena;
        }
    }

  
}

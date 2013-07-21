using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Db4objects.Db4o;

namespace Db4oEntidades.Extensions
{
    /// <summary>
    /// Extensão para tratamentos de IRepositorio
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Copia o estado de um objeto para um ExpandoObject
        /// </summary>
        /// <param name="anonymousObject">Objeto origem</param>
        /// <returns></returns>
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> d = new ExpandoObject();
            foreach (PropertyInfo property in anonymousObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                d[property.Name] = property.GetValue(anonymousObject);

            return d as ExpandoObject;
        }

        /// <summary>
        /// Copia o estado de uma lista IObjectSet para uma lista de ExpandoObject
        /// </summary>
        /// <param name="lista">Lista Origem</param>
        /// <returns>Lista de ExpandoObject</returns>
        public static List<ExpandoObject> ToExpandoList(this IEnumerable lista)
        {
            return (from object o in lista select o.ToExpando()).ToList();
        }
    }
}

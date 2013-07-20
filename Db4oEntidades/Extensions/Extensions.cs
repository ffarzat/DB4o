using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

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
            foreach (PropertyInfo property in anonymousObject.GetType().GetProperties())
                d[property.Name] = property.GetValue(anonymousObject, null);

            return d as ExpandoObject;
        }
    }
}

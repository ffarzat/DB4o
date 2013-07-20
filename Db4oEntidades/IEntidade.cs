using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db4oEntidades
{

    /// <summary>
    /// Interface pública de Entidade no Framework de CRUD e no de Processo
    /// </summary>
    /// <typeparam name="T">Tipo de Id da Entiade (GUID, Int, String)</typeparam>
    public class IEntidade<T>
    {
        /// <summary>
        /// Id da Entidade
        /// </summary>
        T Id { get; set; }
    }
}

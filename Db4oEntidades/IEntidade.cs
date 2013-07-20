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
    public interface IEntidade
    {
        /// <summary>
        /// Id da Entidade (GUID)
        /// </summary>
        Guid Id { get; set; }

    }
}

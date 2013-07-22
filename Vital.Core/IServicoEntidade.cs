using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Core
{
    /// <summary>
    /// Interface para os Serviços de Domínio Concretos
    /// </summary>
    public interface IServicoEntidade
    {

        /// <summary>
        /// Dada uma string com o nome do Tipo retorna uma instância do mesmo
        /// </summary>
        /// <param name="tipo">String com o Nome do Tipo. Ex.: PreInscrito</param>
        /// <returns>
        /// Instância vazia do Tipo
        /// </returns>
        IEntidade ObterAnonimoDe(string tipo);

        /// <summary>
        /// Copia o estado armazendo em um ExpandoObject para um tipo anônimo de destino
        /// </summary>
        /// <param name="origem">Dados do objeto</param>
        /// <param name="destino">Tipo anônimo representando o Objeto</param>
        /// <returns>O tipo anônimo preenchido</returns>
        IEntidade CopiarEstadoDeObjeto(ExpandoObject origem, object destino);

        //TODO: Implementar
        // validar Estado da Entidade
        // validar inserção
        // validar alteração
        // validar exclusão
    }
}

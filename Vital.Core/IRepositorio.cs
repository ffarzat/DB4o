using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Vital.Core
{
    /// <summary>
    /// Representa um Repositório do Framework da Vital
    /// </summary>
    /// <remarks>
    /// Essa interface estaria no Vital.Core
    /// </remarks>
    public interface IRepositorio
    {

        /// <summary>
        /// Obtem a instância da Entidade por suas propriedades
        /// </summary>
        /// <param name="entidade">Instância representa o tipo da Entidade com as propriedades preenchidas</param>
        /// <returns>
        /// Instância armazenada da Entidade
        /// </returns>
        IEntidade Obter(IEntidade entidade);

        /// <summary>
        /// Obtem uma lista  da Entidade pelo seu Tipo
        /// </summary>
        /// <param name="entidade">Instância que representa o tipo da Entidade</param>
        /// <returns>
        /// Lista completa de Entidades correspondentes
        /// </returns>
        List<IEntidade> Listar(IEntidade entidade);

        /// <summary>
        /// Obtem uma lista de objetos que representam a entidade
        /// </summary>
        /// <param name="entidade">Instância que representa o tipo da Entidade</param>
        /// <param name="numeroDaPagina">Número da página a ser consultada</param>
        /// <param name="numeroDeRegistrosPorPagina">Quantos registros por página</param>
        /// <param name="propriedadeOrdenar">Nome da propriedade do objeto para ordenar</param>
        /// <param name="direcaoOrdenar">Direção da ordenação (ASC ou DESC)</param>
        /// <returns>
        /// Lista completa de Entidades correspondentes à consulta (paginado e ordenado)
        /// </returns>
        ResultadoConsulta Listar(IEntidade entidade, int numeroDaPagina, int numeroDeRegistrosPorPagina, string propriedadeOrdenar, string direcaoOrdenar);

        /// <summary>
        /// Adiciona uma Nova Entidade do Repositório 
        /// </summary>
        /// <param name="entidade">Instância da Entidade</param>
        /// <returns>
        /// A instância com o Id gerado
        /// </returns>
        IEntidade Inserir(IEntidade entidade);

        /// <summary>
        /// Edita os dados de uma Entidade
        /// </summary>
        /// <param name="entidade">Instância da Entidade</param>
        void Alterar(IEntidade entidade);

        /// <summary>
        /// Apaga uma Entidade do Repositório
        /// </summary>
        /// <param name="entidade">Instância da Entidade</param>
        void Excluir(IEntidade entidade);

        /// <summary>
        /// Obriga o Repositório a liberar os recursos
        /// </summary>
        void Dispose();

        //TODO: Implementar
        #region Para implementar
        //void Commit();

        //void Rollback();
        #endregion

    }
}

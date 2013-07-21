using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Db4oEntidades
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
        /// Obtem uma entidade por Id
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="id">Identificador da Entidade</param>
        /// <returns>ExpandoObject representando a entidade</returns>
        ExpandoObject ObterPor(string entidade, Guid id);

        /// <summary>
        /// Obtem uma lista de objetos que representam a entidade
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <returns></returns>
        List<ExpandoObject> Listar(string entidade);

        /// <summary>
        /// Obtem uma lista de objetos que representam a entidade
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="numeroDaPagina">Número da página a ser consultada</param>
        /// <param name="numeroDeRegistrosPorPagina">Quantos registros por página</param>
        /// <param name="campoOrdenar">Nome da propriedade do objeto para ordenar</param>
        /// <param name="direcaoOrdenar">Direção da ordenação (ASC ou DESC)</param>
        /// <returns></returns>
        ResultadoConsulta Listar(string entidade, int numeroDaPagina, int numeroDeRegistrosPorPagina, string campoOrdenar, string direcaoOrdenar);

        /// <summary>
        /// Adiciona uma nova Entidade ao Repositório
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="conteudo">ExpandoObject com os dados da nova Entidade</param>
        /// <returns>Anônimo com os dados inseridos e um Id gerado para essa Entidade</returns>
        ExpandoObject Inserir(string entidade, ExpandoObject conteudo);

        /// <summary>
        /// Edita os dados de uma Entidade
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="conteudo">ExpandoObject com os dados da  Entidade</param>
        /// <returns>ExpandoObject com os dados editados</returns>
        ExpandoObject Alterar(string entidade, ExpandoObject conteudo);

        /// <summary>
        /// Apaga uma Entidade do Repositório
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="id">identificador da  Entidade</param>
        /// <returns>ExpandoObject representando a Entidade Excluída</returns>
        ExpandoObject Excluir(string entidade, Guid id);

        /// <summary>
        /// Obriga o Repositório a liberar os recursos
        /// </summary>
        void Dispose();
    }
}

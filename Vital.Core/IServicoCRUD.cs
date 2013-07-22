using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Vital.Core
{
    /// <summary>
    /// Representa um Serviço de Domínimo que trata as operacoes de CRUD
    /// </summary>
    public interface IServicoCrud
    {
        /// <summary>
        /// Obtem uma entidade dado os valores de propriedades de uma instância de exemplo
        /// </summary>
        /// <param name="entidade">Nome do Tipo da Entidade</param>
        /// <param name="conteudo">Instância com os valores da consulta</param>
        /// <returns></returns>
        IEntidade Obter(string entidade, object conteudo);

        /// <summary>
        /// Obtem uma lista do Tipo informado
        /// </summary>
        /// <param name="entidade">Nome do Tipo da Entidade</param>
        /// <returns></returns>
        List<IEntidade> Listar(string entidade);

        /// <summary>
        /// Obtem uma lista do Tipo informado paginando conforme as opções
        /// </summary>
        /// <param name="entidade">Nome do Tipo da Entidade</param>
        /// <param name="numeroDaPagina">Página Atual</param>
        /// <param name="numeroDeRegistrosPorPagina">Quantidade de Registros por página</param>
        /// <param name="propriedadeOrdenar">Nome da propriedade da Entidade para realizar ordenação</param>
        /// <param name="direcaoOrdenar">"ASC" ou "DESC"</param>
        /// <returns></returns>
        ResultadoConsulta Listar(string entidade, int numeroDaPagina, int numeroDeRegistrosPorPagina, string propriedadeOrdenar, string direcaoOrdenar);

        /// <summary>
        /// Adiciona uma Nova Entidade do Repositório 
        /// </summary>
        /// <param name="tipo">Nome do Tipo da Entidade</param>
        /// <param name="entidade">Instância representando a Entidade</param>
        /// <returns>
        /// A instância com o Id gerado
        /// </returns>
        IEntidade Inserir(string tipo, object entidade);

        /// <summary>
        /// Atualiza uma entidade no repositório
        /// </summary>
        /// <param name="tipo">Nome do Tipo da Entidade</param>
        /// <param name="entidade">Instância representando a Entidade</param>
        void Alterar(string tipo, object entidade);

        /// <summary>
        /// Apaga uma Entidade do Repositório conforme os dados da instância passada
        /// </summary>
        /// <param name="tipo">Nome do Tipo da Entidade</param>
        /// <param name="entidade">Instância com os valores da consulta</param>
        void Excluir(string tipo, object entidade);

        /// <summary>
        /// Libera os recursos que o repositório usa
        /// </summary>
        void Dispose();


    }
}

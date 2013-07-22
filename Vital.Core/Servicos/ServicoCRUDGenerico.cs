using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

using Vital.Core.Extensions;

namespace Vital.Core.Servicos
{
    /// <summary>
    /// Serviço que faz a ponte entre os Repositórios concretos e os Domínios concretos. 
    /// </summary>
    /// <remarks>
    /// Funciona como uma fachada interna e externa desacoplando o Repositório concreto das entidade concretas
    /// </remarks>
    public class ServicoCrudGenerico : IServicoCrud
    {

        #region IServicoCrud
        /// <summary>
        /// Serviço específico do domínimo configurado
        /// </summary>
        private IServicoEntidade _servicoConcreto;

        /// <summary>
        /// Instância única do Repositório
        /// </summary>
        private static IRepositorio _repoInstance;

        /// <summary>
        /// Cria uma nova instância do Serviço de CRUD de Entidades
        /// </summary>
        public ServicoCrudGenerico(Guid IdConvenio)
        {
            //TODO: Colocar um Ioc que nos facilite a configuração, pode ser o mesmo esquema do Gerenciador de Assemblies do Framework de Processos
            _servicoConcreto = resolverServicoConcreto();
            _repoInstance = ObterInstanciaDoRepositorio(IdConvenio);
        }

        /// <summary>
        /// Gambiarra por conta de pressa: resolve o serviço na dll do Mongeral.Core
        /// </summary>
        /// <returns></returns>
        private IServicoEntidade resolverServicoConcreto()
        {
            var dll = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + @"\Mongeral.Core.dll");
            var tipos = dll.GetTypes();
            var classesConcretas = tipos.Where(x => (typeof(IServicoEntidade).IsAssignableFrom(x) && !x.IsAbstract) || x.BaseType.Name == typeof(IServicoEntidade).Name);

            return Activator.CreateInstance(classesConcretas.First()) as IServicoEntidade;
        }

        /// <summary>
        /// Gambiarra por conta de pressa: resolve o Repositorio na dll do RepositorioDb4o
        /// </summary>
        /// <returns></returns>
        private static IRepositorio resolverRepositorioConcreto(Guid idConvenio)
        {
            var dll = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + @"\Db4oEntidades.dll");
            var tipos = dll.GetTypes();
            var classesConcretas = tipos.Where(x => (typeof(IRepositorio).IsAssignableFrom(x) && !x.IsAbstract) || x.BaseType.Name == typeof(IRepositorio).Name);

            return Activator.CreateInstance(classesConcretas.First(), new object[] { idConvenio = idConvenio}) as IRepositorio;
        }

        /// <summary>
        /// Instância do Respositório para uso. SingleTon no caso do db4o
        /// </summary>
        /// <returns>
        /// Instância do Repositório
        /// </returns>
        public static IRepositorio ObterInstanciaDoRepositorio(Guid idConvenio)
        {
            return _repoInstance ?? (_repoInstance = resolverRepositorioConcreto(idConvenio));
        }
#endregion

        #region IRepositorio

        /// <summary>
        /// Obtem uma entidade dado os valores de propriedades de uma instância de exemplo
        /// </summary>
        /// <param name="entidade">Nome do Tipo da Entidade</param>
        /// <param name="conteudo">Instância com os valores da consulta</param>
        /// <returns></returns>
        public IEntidade Obter(string entidade, object conteudo)
        {
            IEntidade entidadeParaConsultar = _servicoConcreto.ObterAnonimoDe(entidade);
            return _repoInstance.Obter(entidadeParaConsultar);
        }

        /// <summary>
        /// Obtem uma lista do Tipo informado
        /// </summary>
        /// <param name="entidade">Nome do Tipo da Entidade</param>
        /// <returns></returns>
        public List<IEntidade> Listar(string entidade)
        {
            IEntidade entidadeParaConsultar =_servicoConcreto.ObterAnonimoDe(entidade);
            return _repoInstance.Listar(entidadeParaConsultar);
        }

        /// <summary>
        /// Obtem uma lista do Tipo informado paginando conforme as opções
        /// </summary>
        /// <param name="entidade">Nome do Tipo da Entidade</param>
        /// <param name="numeroDaPagina">Página Atual</param>
        /// <param name="numeroDeRegistrosPorPagina">Quantidade de Registros por página</param>
        /// <param name="propriedadeOrdenar">Nome da propriedade da Entidade para realizar ordenação</param>
        /// <param name="direcaoOrdenar">"ASC" ou "DESC"</param>
        /// <returns></returns>
        public ResultadoConsulta Listar(string entidade, int numeroDaPagina, int numeroDeRegistrosPorPagina, string propriedadeOrdenar, string direcaoOrdenar)
        {
            IEntidade entidadeParaConsultar = _servicoConcreto.ObterAnonimoDe(entidade);
            return _repoInstance.Listar(entidadeParaConsultar, numeroDaPagina, numeroDeRegistrosPorPagina, propriedadeOrdenar, direcaoOrdenar);
        }

        /// <summary>
        /// Adiciona uma Nova Entidade do Repositório 
        /// </summary>
        /// <param name="tipo">Nome do Tipo da Entidade</param>
        /// <param name="entidade">Instância representando a Entidade</param>
        /// <returns>
        /// A instância com o Id gerado
        /// </returns>
        public IEntidade Inserir(string tipo, object entidade)
        {
            IEntidade entidadeParaIncluir =_servicoConcreto.ObterAnonimoDe(tipo);
            _servicoConcreto.CopiarEstadoDeObjeto(entidade.ToExpando(), entidadeParaIncluir);
            return _repoInstance.Inserir(entidadeParaIncluir);
        }

        /// <summary>
        /// Edita os dados de uma Entidade
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="entidade">Instância da Entidade</param>
        public void Alterar(string tipo, object entidade)
        {
            IEntidade entidadeParaIncluir = _servicoConcreto.ObterAnonimoDe(tipo);
            _servicoConcreto.CopiarEstadoDeObjeto(entidade.ToExpando(), entidadeParaIncluir);
            _repoInstance.Alterar(entidadeParaIncluir as IEntidade);
        }

        /// <summary>
        /// Apaga uma Entidade do Repositório
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="entidade">Instância da Entidade</param>
        public void Excluir(string tipo, object entidade)
        {
            IEntidade entidadeParaExcluir = _servicoConcreto.ObterAnonimoDe(tipo);
            entidadeParaExcluir.Id = (entidade as IEntidade).Id;
            _repoInstance.Excluir(entidadeParaExcluir);
        }

        /// <summary>
        /// Obriga o Repositório a liberar os recursos
        /// </summary>
        public void Dispose()
        {
            _repoInstance.Dispose();
        }

        #endregion

    }
}

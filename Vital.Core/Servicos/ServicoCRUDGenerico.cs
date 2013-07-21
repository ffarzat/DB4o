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
        /// Dada uma string com o nome do Tipo retorna uma instância do mesmo
        /// </summary>
        /// <param name="tipo">String com o Nome do Tipo. Ex.: PreInscrito</param>
        /// <returns>
        /// Instância vazia do Tipo
        /// </returns>
        public IEntidade ObterAnonimoDe(string tipo)
        {
            //TODO: Colocar DBC da Vital.Infra
            if (tipo == null) throw new ArgumentNullException("tipo");
            return _servicoConcreto.ObterAnonimoDe(tipo);
        }

        /// <summary>
        /// Copia o estado armazendo em um ExpandoObject para um tipo anônimo de destino
        /// </summary>
        /// <param name="origem">Dados do objeto</param>
        /// <param name="destino">Tipo anônimo representando o Objeto</param>
        /// <returns>O tipo anônimo preenchido</returns>
        public object CopiarEstadoDeObjeto(ExpandoObject origem, object destino)
        {
            //TODO: Colocar DBC da Vital.Infra
            if (origem == null) throw new ArgumentNullException("origem");
            if (destino == null) throw new ArgumentNullException("destino");

            return _servicoConcreto.CopiarEstadoDeObjeto(origem, destino);
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
        /// <remarks>
        /// TODO: Isso aqui deveria ficar fora dessa classe. Pensar num serviço de domínio que sabe recuperar Repositórios Concretos
        /// </remarks>
        public static IRepositorio ObterInstanciaDoRepositorio(Guid idConvenio)
        {
            return _repoInstance ?? (_repoInstance = resolverRepositorioConcreto(idConvenio));
        }

        #region IRepositorio
        /// <summary>
        /// Obtem a instância da Entidade por suas propriedades
        /// </summary>
        /// <param name="entidade">Instância representa o tipo da Entidade com as propriedades preenchidas</param>
        /// <returns>
        /// Instância armazenada da Entidade
        /// </returns>
        public IEntidade Obter(IEntidade entidade)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtem uma lista  da Entidade pelo seu Tipo
        /// </summary>
        /// <param name="entidade">Instância que representa o tipo da Entidade</param>
        /// <returns>
        /// Lista completa de Entidades correspondentes
        /// </returns>
        public List<IEntidade> Listar(IEntidade entidade)
        {
            throw new NotImplementedException();
        }

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
        /// </returns
        public ResultadoConsulta Listar(IEntidade entidade, int numeroDaPagina, int numeroDeRegistrosPorPagina, string propriedadeOrdenar, string direcaoOrdenar)
        {
            throw new NotImplementedException();
        }


        public IEntidade Inserir(string tipo, object entidade)
        {
            IEntidade entidadeParaIncluir =_servicoConcreto.ObterAnonimoDe(tipo);
            _servicoConcreto.CopiarEstadoDeObjeto(entidade.ToExpando(), entidadeParaIncluir);
            return _repoInstance.Inserir(entidadeParaIncluir);
        }

        /// <summary>
        /// Edita os dados de uma Entidade
        /// </summary>
        /// <param name="entidade">Instância da Entidade</param>
        public void Alterar(IEntidade entidade)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Apaga uma Entidade do Repositório
        /// </summary>
        /// <param name="entidade">Instância da Entidade</param>
        public void Excluir(IEntidade entidade)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obriga o Repositório a liberar os recursos
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}

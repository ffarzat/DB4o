using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Vital.Core;
using Vital.Core.Extensions;

namespace Db4oEntidades
{
    /// <summary>
    /// Repositório específico para trabalhar com o DB4o
    /// </summary>
    class RepositorioDb4O : IRepositorio
    {
        /// <summary>
        /// Construtor recebendo o IdConvenio para trabalhar
        /// </summary>
        /// <param name="idConvenio"></param>
        public RepositorioDb4O(Guid idConvenio)
        {
            this._idConvenio = idConvenio;
        }

        #region IRepositorio

        /// <summary>
        /// Obtem a instância da Entidade por Id
        /// </summary>
        /// <param name="entidade">Instância Vazia que representa o tipo da Entidade</param>
        /// <returns></returns>
        public IEntidade Obter(IEntidade entidade)
        {
            return Context.QueryByExample(entidade).Cast<IEntidade>().Single();
        }
        
        /// <summary>
        /// Obtem uma lista  da Entidade pelo seu Tipo
        /// </summary>
        /// <param name="entidade">Instância Vazia que representa o tipo da Entidade</param>
        /// <returns>
        /// Lista de Entidades correspondente
        /// </returns>
        public List<IEntidade> Listar(IEntidade entidade)
        {
            return Context.Query(entidade.GetType()).Cast<IEntidade>().ToList();
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
        /// </returns>
        public ResultadoConsulta Listar(IEntidade entidade, int numeroDaPagina, int numeroDeRegistrosPorPagina, string propriedadeOrdenar, string direcaoOrdenar)
        {
            List<IEntidade> resultadoConsulta = Context.Query(entidade.GetType()).Cast<IEntidade>().ToList();

            var resultadoPaginado = PaginarResultado(resultadoConsulta, numeroDaPagina, numeroDeRegistrosPorPagina);

            var resultadoOrdenado = OrdenarResultado(resultadoPaginado, propriedadeOrdenar, direcaoOrdenar);

            int totalDeRegistros = resultadoConsulta.Count;
            int totalDePaginas = (totalDeRegistros / numeroDeRegistrosPorPagina);

            return new ResultadoConsulta(totalDePaginas, totalDeRegistros, resultadoOrdenado);
        }

        /// <summary>
        /// Adiciona uma Nova Entidade do Repositório 
        /// </summary>
        /// <param name="entidade">Instância da Entidade</param>
        /// <returns>
        /// A instância com o Id gerado
        /// </returns>
        public IEntidade Inserir(IEntidade entidade)
        {
            ((IEntidade)entidade).Id = Guid.NewGuid();
            Context.Store(entidade);
            return entidade;
        }

        /// <summary>
        /// Edita os dados de uma Entidade
        /// </summary>
        /// <param name="entidade">Instância da Entidade</param>
        public void Alterar(IEntidade entidade)
        {
            Excluir(entidade);
            Inserir(entidade);
        }

        /// <summary>
        /// Apaga uma Entidade do Repositório
        /// </summary>
        /// <param name="entidade">Instância da Entidade</param>
        public void Excluir(IEntidade entidade)
        {
            Context.Delete(Obter(entidade));
        }

        /// <summary>
        /// Pagina uma consulta
        /// </summary>
        /// <param name="resultadoConsulta">Lista de objetos</param>
        /// <param name="numeroDaPagina">Página de dados atual</param>
        /// <param name="numeroDeRegistrosPorPagina">Quantidade de registros por página</param>
        /// <returns></returns>
        private List<IEntidade> PaginarResultado(IEnumerable<IEntidade> resultadoConsulta, int numeroDaPagina, int numeroDeRegistrosPorPagina)
        {
            return (from object o in resultadoConsulta select o).Skip(numeroDeRegistrosPorPagina * numeroDaPagina).Take(numeroDeRegistrosPorPagina).Cast<IEntidade>().ToList();
        }

        /// <summary>
        /// Ordena um IEnumerable<object> pelo campo e direção 
        /// </summary>
        /// <param name="resultadoPaginado">IEnumerable<object> com a lista de objetos</param>
        /// <param name="campoOrdenar">Nome da Propriedade no objeto</param>
        /// <param name="direcaoOrdenar">"asc" ou "desc"</param>
        /// <returns>
        /// Lista ordenada
        /// </returns>
        private List<IEntidade> OrdenarResultado(IEnumerable<IEntidade> resultadoPaginado, string campoOrdenar, string direcaoOrdenar)
        {
            return direcaoOrdenar.ToLower().Equals("desc") ? (resultadoPaginado.AsQueryable().OrderByDescending(o => o.ObterValorPropriedade(campoOrdenar))).Cast<IEntidade>().ToList() : (resultadoPaginado.AsQueryable().OrderBy(o => o.ObterValorPropriedade(campoOrdenar))).Cast<IEntidade>().ToList();
        }

        #endregion

        #region Implementação Específica do DB4o

        private Guid _idConvenio;

        /// <summary>
        /// Arquivo de dados desse Repositório
        /// </summary>
        /// <remarks>
        /// </remarks>
        private const string Dbname = "{0}.yap";

        /// <summary>
        /// Container de objetos do DB4o
        /// </summary>
        IObjectContainer _context = null;

        /// <summary>
        /// Instância do ObjectContainer para uso
        /// </summary>
        private IObjectContainer Context
        {
            get
            {
                if (_context == null)
                {
                    //Habilitando o uso de UUID
                    IConfiguration configuration = Db4oFactory.NewConfiguration();
                    configuration.GenerateUUIDs(ConfigScope.Globally);

                    _context = Db4oFactory.OpenFile(string.Format(Dbname, this._idConvenio));
                }
                return _context;
            }
        }

        /// <summary>
        /// Libera o objeto para o Garbage Collector
        /// </summary>
        public void Dispose()
        {
            _context.Close();
            _context.Dispose();
            _context = null;
        }

        #endregion
    }
}

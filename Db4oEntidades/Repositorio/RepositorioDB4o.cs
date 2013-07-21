using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4oEntidades.Extensions;
using Db4objects.Db4o.Constraints;
using Db4objects.Db4o.Ext;


namespace Db4oEntidades.Repositorio
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
        /// Obtem uma entidade por Id
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="id">Identificador da Entidade</param>
        /// <returns>ExpandoObject representando a entidade</returns>
        public ExpandoObject ObterPor(string entidade, Guid id)
        {
            var instanciaDoTipoParaFazerMapeamento = ObterAnonimoDe(entidade);

            ((IEntidade)instanciaDoTipoParaFazerMapeamento).Id = id;

            IObjectSet result = Context.QueryByExample(instanciaDoTipoParaFazerMapeamento);

            if (result.Count == 0)
                return null;

            return result[0] .ToExpando();
        }

        /// <summary>
        /// Obtem uma lista de objetos que representam a entidade
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <returns></returns>
        public List<ExpandoObject> Listar(string entidade)
        {
            var instanciaDoTipoParaFazerMapeamento = ObterAnonimoDe(entidade);
            IObjectSet result = Context.Query(instanciaDoTipoParaFazerMapeamento.GetType());

            return result.ToExpandoList();
        }

        /// <summary>
        /// Obtem uma lista de objetos que representam a entidade
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="numeroDaPagina">Número da página a ser consultada</param>
        /// <param name="numeroDeRegistrosPorPagina">Quantos registros por página</param>
        /// <param name="campoOrdenar">Nome da propriedade do objeto para ordenar</param>
        /// <param name="direcaoOrdenar">Direção da ordenação (ASC ou DESC)</param>
        /// <returns></returns>
        public ResultadoPaginacao Listar(string entidade, int numeroDaPagina, int numeroDeRegistrosPorPagina, string campoOrdenar, string direcaoOrdenar)
        {
            var instanciaDoTipoParaFazerMapeamento = ObterAnonimoDe(entidade);
            IObjectSet result = Context.Query(instanciaDoTipoParaFazerMapeamento.GetType());
            int totalDeRegistros = result.Count;
            int totalDePaginas = (totalDeRegistros/numeroDeRegistrosPorPagina);

            var resultadoPaginado =  (from object o in result select o).Skip(numeroDeRegistrosPorPagina*numeroDaPagina).Take(numeroDeRegistrosPorPagina);

            resultadoPaginado =  direcaoOrdenar.ToLower().Equals("desc")? 
                resultadoPaginado.AsQueryable().OrderByDescending(o => o.ObterValorPropriedade(campoOrdenar))
                :resultadoPaginado.AsQueryable().OrderBy(o => o.ObterValorPropriedade(campoOrdenar));

            return new ResultadoPaginacao(totalDePaginas, totalDeRegistros, resultadoPaginado.ToExpandoList());
        }

        /// <summary>
        /// Adiciona uma nova Entidade ao Repositório
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="conteudo">ExpandoObject com os dados da nova Entidade</param>
        /// <returns>ExpandoObject com os dados inseridos e um Id gerado para essa Entidade</returns>
        public ExpandoObject Inserir(string entidade, ExpandoObject conteudo)
        {
            IDictionary<string, Object> dadosdaEntidade = conteudo;

            //TODO: Pensar numa maneira de Pedir ao Domínio para validar a Entidade
            var instanciaDoTipoParaFazerMapeamento = ObterAnonimoDe(entidade);

            //Transformar no tipo com o intuito de validar se o objeto é da estrutura certa
            var novo = CopiarEstadoDeObjeto(conteudo, instanciaDoTipoParaFazerMapeamento);
            
            //Testar se a Entidade está válida para ser adicionada ao repositório
            //Aqui valida por exemplo se já tem id

            ((IEntidade) novo).Id = Guid.NewGuid();

            //Salvar
            Context.Store(novo);
           
            return novo.ToExpando();
        }

        /// <summary>
        /// Edita os dados de uma Entidade
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="conteudo">ExpandoObject com os dados da  Entidade</param>
        /// <returns>ExpandoObject com os dados editados</returns>
        public ExpandoObject Alterar(string entidade, ExpandoObject conteudo)
        {
            IDictionary<string, Object> dadosdaEntidade = conteudo;
            Excluir(entidade, Guid.Parse(dadosdaEntidade["Id"].ToString()));
            Inserir(entidade, conteudo);
            return conteudo;
        }

        /// <summary>
        /// Apaga uma Entidade do Repositório
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="id">identificador da  Entidade</param>
        /// <returns>ExpandoObject representando a Entidade Excluída</returns>
        public ExpandoObject Excluir(string entidade, Guid id)
        {
            var instanciaDoTipoParaFazerMapeamento = ObterAnonimoDe(entidade);
            ((IEntidade)instanciaDoTipoParaFazerMapeamento).Id = id;
            IObjectSet result = Context.QueryByExample(instanciaDoTipoParaFazerMapeamento);

            var excluidoRetorno = CopiarEstadoDeObjeto(result[0].ToExpando(), instanciaDoTipoParaFazerMapeamento);

            Context.Delete(result[0]);

            return excluidoRetorno.ToExpando();
        }

        #endregion

        #region Implementação Específica do DB4o

        private Guid _idConvenio;

        /// <summary>
        /// Dada uma String descrevendo o FullName de um tipo qualquer retorna uma instância anônima que representa o mesmo
        /// </summary>
        /// <param name="tipo">FullName do Tipo</param>
        /// <returns>Instância anônima que representa o Tipo</returns>
        private object ObterAnonimoDe(string tipo)
        {
            //TODO: Colocar DBC da Vital.Infra
            if (tipo == null) throw new ArgumentNullException("tipo");

            Type tipoParaAnonimo = Assembly.GetExecutingAssembly().GetType(tipo);
            return Activator.CreateInstance(tipoParaAnonimo);
        }

        /// <summary>
        /// Copia o estado armazendo em um ExpandoObject para um tipo anônimo de destino
        /// </summary>
        /// <param name="origem">Dados do objeto</param>
        /// <param name="destino">Tipo anônimo representando o Objeto</param>
        /// <returns>O tipo anônimo preenchido</returns>
        private object CopiarEstadoDeObjeto(ExpandoObject origem, object destino)
        {
            IDictionary<string, object> expando = origem;

            foreach (var propriedade in destino.GetType().GetProperties())
            {
                var lower = propriedade.Name.ToLower();
                var key = expando.Keys.SingleOrDefault(k => k.ToLower() == lower);

                if (key != null)
                {
                    switch (propriedade.PropertyType.Name)
                    {
                        case "Guid":
                            propriedade.SetValue(destino, Guid.Parse(expando[key].ToString()), null);
                            break;
                        default:
                            propriedade.SetValue(destino, expando[key], null);
                            break;
                    }
                }
            }

            return destino;
        }

        /// <summary>
        /// Arquivo de dados desse Repositório
        /// </summary>
        /// <remarks>
        /// </remarks>
        private const string Dbname = "{0}.yap";

        /// <summary>
        /// Instância única do Repositório
        /// </summary>
        private static IRepositorio _repoInstance;

        /// <summary>
        /// Container de objetos do DB4o
        /// </summary>
        IObjectContainer _context = null;

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
            if (_repoInstance == null)
                _repoInstance = new RepositorioDb4O(idConvenio);

            return _repoInstance;
        }

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

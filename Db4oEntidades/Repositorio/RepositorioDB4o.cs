using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Db4objects.Db4o;

namespace Db4oEntidades.Repositorio
{
    /// <summary>
    /// Repositório específico para trabalhar com o DB4o
    /// </summary>
    class RepositorioDb4O : IRepositorio, IDisposable
    {

        #region IRepositorio

        /// <summary>
        /// Obtem uma entidade por Id
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="id">Identificador da Entidade</param>
        /// <returns>ExpandoObject representando a entidade</returns>
        public ExpandoObject ObterPor(string entidade, Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtem uma lista de objetos que representam a entidade
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <returns></returns>
        public List<ExpandoObject> Listar(string entidade)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adiciona uma nova Entidade ao Repositório
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="conteudo">ExpandoObject com os dados da nova Entidade</param>
        /// <returns>ExpandoObject com os dados inseridos e um Id gerado para essa Entidade</returns>
        public ExpandoObject Inserir(string entidade, ExpandoObject conteudo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Edita os dados de uma Entidade
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="conteudo">ExpandoObject com os dados da  Entidade</param>
        /// <returns>ExpandoObject com os dados editados</returns>
        public ExpandoObject Alterar(string entidade, ExpandoObject conteudo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Apaga uma Entidade do Repositório
        /// </summary>
        /// <param name="entidade">Nome da entidade no Domínio</param>
        /// <param name="id">identificador da  Entidade</param>
        /// <returns>ExpandoObject representando a Entidade Excluída</returns>
        public ExpandoObject Excluir(string entidade, Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementação Específica do DB4o

        /// <summary>
        /// Instância única do Repositório
        /// </summary>
        private static IRepositorio _repoInstance;

        /// <summary>
        /// Arquivo de dados desse Repositório
        /// </summary>
        /// <remarks>
        /// TODO: Pensar em padrão para forçar passar o idConvenio para obter o repositorio concreto
        /// </remarks>
        public const string Dbname = "IdConvenio.dat";

        /// <summary>
        /// Container de objetos do DB4o
        /// </summary>
        IObjectContainer _context = null;

        /// <summary>
        /// Instância do Respositório para uso
        /// </summary>
        /// <returns></returns>
        public static IRepositorio GetRepositoryInstance()
        {
            if (_repoInstance == null)
                _repoInstance = new RepositorioDb4O();

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
                    _context = Db4oFactory.OpenFile(Dbname);

                return _context;
            }
        }

        /// <summary>
        /// Libera o objeto para o Garbage Collector
        /// </summary>
        public void Dispose()
        {
            this._context.Close();
            this._context.Dispose();
            this._context = null;
        }

        #endregion
    }
}

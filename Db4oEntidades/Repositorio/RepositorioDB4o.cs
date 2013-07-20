using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db4oEntidades.Repositorio
{
    /// <summary>
    /// Repositório específico para trabalhar com o DB4o
    /// </summary>
    class RepositorioDB4o: IRepositorio
    {
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
    }
}

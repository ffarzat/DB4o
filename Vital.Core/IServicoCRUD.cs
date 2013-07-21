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

        IEntidade Obter(IEntidade entidade);


        List<IEntidade> Listar(IEntidade entidade);


        ResultadoConsulta Listar(IEntidade entidade, int numeroDaPagina, int numeroDeRegistrosPorPagina, string propriedadeOrdenar, string direcaoOrdenar);

        /// <summary>
        /// Adiciona uma Nova Entidade do Repositório 
        /// </summary>
        /// <param name="tipo">String com o Nome do tipo</param>
        /// <param name="entidade">Instância da Entidade</param>
        /// <returns>
        /// A instância com o Id gerado
        /// </returns>
        IEntidade Inserir(string tipo, object entidade);

        void Alterar(IEntidade entidade);


        void Excluir(IEntidade entidade);


        void Dispose();


    }
}

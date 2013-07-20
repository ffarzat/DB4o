using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Db4oEntidades.Extensions;

namespace Db4oEntidades.Testes
{

    /// <summary>
    /// Teste de Infra do Repositório do DB4o
    /// </summary>
    [TestFixture]
    public class DB4oTestes
    {
        [Test]
        public void Abrir_Fechar_Conexao_Com_Mesma_Instancia()
        {
            Repositorio.IRepositorio repositorio = Repositorio.RepositorioDb4O.GetRepositoryInstance();
            Assert.IsNotNull(repositorio, "Deveria voltar uma instância do RepositorioDb4O");

            Repositorio.IRepositorio repositorio2 = Repositorio.RepositorioDb4O.GetRepositoryInstance();
            Assert.AreSame(repositorio, repositorio2, "Deveria ser a mesma instância");
        }

        [Test]
        public void Inserir_Nova_Entidade_Por_String()
        {
            Repositorio.IRepositorio repositorio = Repositorio.RepositorioDb4O.GetRepositoryInstance();
            var preInscrito = new PreInscrito() {NomeDoSegurado = "Teste Unitário da Silva Sauro"};
            var preInscritoInserido = repositorio.Inserir("Db4oEntidades.PreInscrito", preInscrito.ToExpando());

            Assert.IsNotNull(preInscritoInserido);

        }
    }
}

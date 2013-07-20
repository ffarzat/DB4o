using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db4oEntidades.Repositorio;
using NUnit.Framework;
using Db4oEntidades.Extensions;

namespace Db4oEntidades.Testes
{

    /// <summary>
    /// Teste de Infra do Repositório do DB4o
    /// </summary>
    [TestFixture]
    public class Db4OTestes
    {
        private Guid _idConvenio;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _idConvenio = new Guid();
        }

        [Test]
        public void Abrir_Fechar_Conexao_Com_Mesma_Instancia()
        {
            IRepositorio repositorio = RepositorioDb4O.ObterInstanciaDoRepositorio(_idConvenio);
            Assert.IsNotNull(repositorio, "Deveria voltar uma instância do RepositorioDb4O");

            IRepositorio repositorio2 = RepositorioDb4O.ObterInstanciaDoRepositorio(_idConvenio);
            Assert.AreSame(repositorio, repositorio2, "Deveria ser a mesma instância");
        }

        [Test]
        public void Inserir_Nova_Entidade_Por_String()
        {
            IRepositorio repositorio = RepositorioDb4O.ObterInstanciaDoRepositorio(_idConvenio);
            var preInscrito = new PreInscrito() {NomeDoSegurado = "Teste Unitário da Silva Sauro"};
            var preInscritoInserido = repositorio.Inserir("Db4oEntidades.PreInscrito", preInscrito.ToExpando()) as IDictionary<string, Object>; 

            Assert.IsNotNull(preInscritoInserido);
            Assert.AreEqual("Teste Unitário da Silva Sauro", preInscritoInserido["NomeDoSegurado"]);
        }
    }
}

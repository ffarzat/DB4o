using System;
using System.Collections.Generic;
using System.Dynamic;
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
        private string Tipo = "Db4oEntidades.PreInscrito";

        [TestFixtureSetUp]
        public void SetUp()
        {
            _idConvenio = new Guid(); //Valor defult do guid
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            IRepositorio repositorio = RepositorioDb4O.ObterInstanciaDoRepositorio(_idConvenio);
            repositorio.Dispose();

            System.IO.File.Delete(_idConvenio.ToString() + ".yap");
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
        public void Inserir_Recuperar_Alterar_Excluir_Entidade_Por_String()
        {
            IRepositorio repositorio = RepositorioDb4O.ObterInstanciaDoRepositorio(_idConvenio);
            var preInscrito = new PreInscrito() {NomeDoSegurado = "Teste Unitário da Silva Sauro", };
            var preInscritoInserido = repositorio.Inserir(Tipo, preInscrito.ToExpando()) as IDictionary<string, Object>; 

            Assert.IsNotNull(preInscritoInserido);
            Assert.AreEqual("Teste Unitário da Silva Sauro", preInscritoInserido["NomeDoSegurado"]);
            Assert.IsNotNull(preInscritoInserido["Id"]);

            repositorio.Excluir(Tipo, Guid.Parse(preInscritoInserido["Id"].ToString()));
        }

        [Test]
        public void Recuperar_Alterar_Entidade()
        {
            //Inserir via anonimo
            IRepositorio repositorio = RepositorioDb4O.ObterInstanciaDoRepositorio(_idConvenio);
            var preInscrito = new { NomeDoSegurado = "Teste De inserção apenas para alterar depois", };
            var preInscritoInserido = repositorio.Inserir(Tipo, preInscrito.ToExpando()) as IDictionary<string, Object>;

            Assert.IsNotNull(preInscritoInserido);
            Assert.AreEqual("Teste De inserção apenas para alterar depois", preInscritoInserido["NomeDoSegurado"]);
            Assert.IsNotNull(preInscritoInserido["Id"]);

            Guid idGerado = Guid.Parse(preInscritoInserido["Id"].ToString());

            preInscritoInserido["Bairro"] = "Bairro via teste unitário";
            preInscritoInserido["NomeDoSegurado"] = "Nome Alterado";
            var preInscritoAlterado = repositorio.Alterar(Tipo, preInscritoInserido as ExpandoObject) as IDictionary<string, Object>;

            Assert.IsNotNull(preInscritoAlterado);
            Assert.AreEqual("Nome Alterado", preInscritoAlterado["NomeDoSegurado"]);
            Assert.AreEqual(idGerado, preInscritoAlterado["Id"]);
            Assert.AreEqual("Bairro via teste unitário", preInscritoAlterado["Bairro"]);
        }

        [Test]
        public void Inserir_Varios_para_Listar_Paginar_Ordenar()
        {
            Guid idNovoDb = Guid.NewGuid();
            IRepositorio repositorio = RepositorioDb4O.ObterInstanciaDoRepositorio(idNovoDb);

            for (var i = 0; i < 5050; i++)
            {
                var preInscrito = new PreInscrito() {NomeDoSegurado = i.ToString() + " Nome", Numero = i.ToString()};
                repositorio.Inserir(Tipo, preInscrito.ToExpando());
            }

            //Recuperar os registros
            var todosRegistros = repositorio.Listar(Tipo);

            Assert.GreaterOrEqual(todosRegistros.Count, 5000);
            Assert.AreEqual((todosRegistros[978] as IDictionary<string, Object>)["NomeDoSegurado"], "978 Nome");

            //Paginar o resultado

            var registrosPaginados = repositorio.Listar(Tipo, 1, 10, "Numero", "desc");

            Assert.AreEqual(registrosPaginados.TotalDePaginas, 505);
            Assert.AreEqual(registrosPaginados.TotalDeRegistros, 5050);
            Assert.AreEqual((registrosPaginados.ListaRegistros[0] as IDictionary<string, Object>)["Numero"], "19");

            
        }

    }
}

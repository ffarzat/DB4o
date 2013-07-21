using System;
using NUnit.Framework;
using Vital.Core.Servicos;

namespace Vital.Core.Testes
{

    /// <summary>
    /// Teste de Infra do Repositório do DB4o
    /// </summary>
    [TestFixture]
    public class ServicoCrudGenericoTestes
    {
        private Guid _idConvenio;
        private string Tipo;
        private IServicoCrud _servico;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _idConvenio = Guid.NewGuid();
            Tipo = "Mongeral.Core.Entidades.PreInscrito";
            _servico = new ServicoCrudGenerico(_idConvenio);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
           _servico.Dispose();
           System.IO.File.Delete(_idConvenio.ToString() + ".yap");
        }


        [Test]
        public void Inserir_Excluir_Entidade_Por_String()
        {

            var preInscrito = new  { NomeDoSegurado = "Teste Unitário da Silva Sauro", };

            var preInscritoInserido = _servico.Inserir(Tipo, preInscrito);

            Assert.IsNotNull(preInscritoInserido);
            Assert.IsNotNull(preInscritoInserido.Id);

            //repositorio.Excluir(Tipo, Guid.Parse(preInscritoInserido["Id"].ToString()));

            //var preInscritoExcluido = repositorio.Obter(Tipo, Guid.Parse(preInscritoInserido["Id"].ToString()));
            //Assert.IsNull(preInscritoExcluido);
        }

        //[Test]
        //public void Recuperar_Alterar_Entidade()
        //{
        //    //Inserir via anonimo
        //    IRepositorio repositorio = RepositorioDb4O.ObterInstanciaDoRepositorio(_idConvenio);
        //    var preInscrito = new { NomeDoSegurado = "Teste De inserção apenas para alterar depois", };
        //    var preInscritoInserido = repositorio.Inserir(Tipo, preInscrito.ToExpando()) as IDictionary<string, Object>;

        //    Assert.IsNotNull(preInscritoInserido);
        //    Assert.AreEqual("Teste De inserção apenas para alterar depois", preInscritoInserido["NomeDoSegurado"]);
        //    Assert.IsNotNull(preInscritoInserido["Id"]);

        //    Guid idGerado = Guid.Parse(preInscritoInserido["Id"].ToString());

        //    preInscritoInserido["Bairro"] = "Bairro via teste unitário";
        //    preInscritoInserido["NomeDoSegurado"] = "Nome Alterado";
        //    var preInscritoAlterado = repositorio.Alterar(Tipo, preInscritoInserido as ExpandoObject) as IDictionary<string, Object>;

        //    Assert.IsNotNull(preInscritoAlterado);
        //    Assert.AreEqual("Nome Alterado", preInscritoAlterado["NomeDoSegurado"]);
        //    Assert.AreEqual(idGerado, preInscritoAlterado["Id"]);
        //    Assert.AreEqual("Bairro via teste unitário", preInscritoAlterado["Bairro"]);
        //}

        //[Test]
        //public void Inserir_Varios_para_Listar_Paginar_Ordenar()
        //{
        //    IRepositorio repositorio = RepositorioDb4O.ObterInstanciaDoRepositorio(_idConvenio);

        //    for (var i = 0; i < 5050; i++)
        //    {
        //        var preInscrito = new {NomeDoSegurado = i.ToString() + " Nome", Numero = i.ToString()};
        //        repositorio.Inserir(Tipo, preInscrito.ToExpando());
        //    }

        //    //Recuperar os registros
        //    var todosRegistros = repositorio.Listar(Tipo);

        //    Assert.GreaterOrEqual(todosRegistros.Count, 5000);
        //    Assert.AreEqual((todosRegistros[978] as IDictionary<string, Object>)["NomeDoSegurado"], "978 Nome");

        //    //Paginar o resultado
        //    var registrosPaginados = repositorio.Listar(Tipo, 1, 10, "Numero", "desc");

        //    Assert.AreEqual(registrosPaginados.TotalDePaginas, 505);
        //    Assert.AreEqual(registrosPaginados.TotalDeRegistros, 5050);
        //    Assert.AreEqual((registrosPaginados.ListaRegistros[0] as IDictionary<string, Object>)["Numero"], "19");
        //}

    }
}

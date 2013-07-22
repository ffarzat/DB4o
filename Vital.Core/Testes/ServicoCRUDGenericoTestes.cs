using System;
using NUnit.Framework;
using Vital.Core.Extensions;
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
            var preInscrito = new  { NomeDoSegurado = "Teste Unitário da Silva Sauro" };

            var preInscritoInserido = _servico.Inserir(Tipo, preInscrito);

            Assert.IsNotNull(preInscritoInserido);
            Assert.IsNotNull(preInscritoInserido.Id);

            _servico.Excluir(Tipo, preInscritoInserido);
            IEntidade preInscritoExcluido = null;

            preInscritoExcluido = _servico.Obter(Tipo, preInscritoInserido);
            Assert.IsNull(preInscritoExcluido);
        }

        [Test]
        public void Recuperar_Alterar_Entidade()
        {
            var preInscrito = new { NomeDoSegurado = "Teste De inserção apenas para alterar depois" };
            var preInscritoInserido = _servico.Inserir(Tipo, preInscrito);

            Assert.IsNotNull(preInscritoInserido);
            Assert.AreEqual("Teste De inserção apenas para alterar depois", preInscritoInserido.ObterValorPropriedade("NomeDoSegurado"));
            Assert.IsNotNull(preInscritoInserido.Id);

            Guid idGerado = preInscritoInserido.Id;

            var preInscritoAlterar = new { NomeDoSegurado = "Nome Alterado", Bairro =  "Bairro via teste unitário", Id = idGerado};
            _servico.Alterar(Tipo, preInscritoAlterar);

            Assert.IsNotNull(preInscritoAlterar);
            Assert.AreEqual("Nome Alterado", preInscritoAlterar.NomeDoSegurado);
            Assert.AreEqual(idGerado, preInscritoAlterar.Id);
            Assert.AreEqual("Bairro via teste unitário", preInscritoAlterar.Bairro);
        }

        [Test]
        public void Inserir_Varios_para_Listar_Paginar_Ordenar()
        {
            for (var i = 0; i < 5050; i++)
            {
                var preInscrito = new { NomeDoSegurado = i.ToString() + " Nome", Numero = i.ToString() };
                _servico.Inserir(Tipo, preInscrito);
            }

            //Recuperar os registros
            var todosRegistros = _servico.Listar(Tipo);

            Assert.GreaterOrEqual(todosRegistros.Count, 5000);
            Assert.AreEqual(todosRegistros[978].ObterValorPropriedade("NomeDoSegurado"), "978 Nome");

            //Paginar o resultado
            var registrosPaginados = _servico.Listar(Tipo, 1, 10, "Numero", "desc");

            Assert.AreEqual(registrosPaginados.TotalDePaginas, 505);
            Assert.AreEqual(registrosPaginados.TotalDeRegistros, 5050);
            Assert.AreEqual(registrosPaginados.ListaRegistros[0].ObterValorPropriedade("Numero"), "19");
        }

    }
}

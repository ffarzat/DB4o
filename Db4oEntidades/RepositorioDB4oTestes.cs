using System;
using NUnit.Framework;
using Vital.Core;


namespace Db4oEntidades
{
    [TestFixture]
    public class RepositorioDb4OTestes
    {
        private Guid _idConvenio;
        private IRepositorio _repo;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _idConvenio = Guid.Parse("d98ca8bc-b0e5-4527-8caa-9a7b727957e5");
            _repo = new RepositorioDb4O(_idConvenio);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _repo.Dispose();
            System.IO.File.Delete(_idConvenio.ToString() + ".yap");
        }

        [Test]
        public void Obter()
        {
            var entidadeinserida = _repo.Inserir(new EntidadeTeste() { Nome = "Fabio de Teste Farzat" });

            var entidadeObtida = _repo.Obter(new EntidadeTeste() { Id = entidadeinserida.Id});

            Assert.AreEqual(entidadeObtida.Id, entidadeinserida.Id);
            Assert.AreEqual((entidadeObtida as EntidadeTeste).Nome, "Fabio de Teste Farzat");
        }

        [Test]
        public void Alterar()
        {
            Guid id;
            var entidadeinserida = _repo.Inserir(new EntidadeTeste() { Nome = "Fabio de Teste Farzat" });

            var entidadeDadosNovos = new EntidadeTeste() { Id = entidadeinserida.Id, Nome="Alterado da Silva Sauro"};

            _repo.Alterar(entidadeDadosNovos);

            var entidadeObtida = _repo.Obter(new EntidadeTeste() { Id = entidadeDadosNovos.Id });

            Assert.AreEqual(entidadeObtida.Id, entidadeinserida.Id);
            Assert.AreEqual((entidadeObtida as EntidadeTeste).Nome, "Alterado da Silva Sauro");
        }
    }

    /// <summary>
    /// Entidade para testes
    /// </summary>
    internal class EntidadeTeste : IEntidade
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
    }
}

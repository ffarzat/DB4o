using System;

namespace Db4oEntidades
{
	[Serializable]
	public class PreInscrito : IEntidade
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

		/// <summary>
		/// ID do convênio de adesão
		/// </summary>
		public Guid IdDoConvenioDeAdesao { get; set; }

        /// <summary>
        /// Nome do Segurado
        /// </summary>
        public string NomeDoSegurado { get; set; }

        /// <summary>
        /// Data de Nascimento
        /// </summary>
        public string DataDeNascimento { get; set; }

        /// <summary>
        /// Sexo do Participante
        /// </summary>
        public string SexoDoParticipante { get; set; }

        /// <summary>
        /// Número de matrícula do servidor
        /// </summary>
        public string NumeroDeMatriculaDoServidor { get; set; }

        /// <summary>
        /// Setor
        /// </summary>
        public string Setor { get; set; }

        /// <summary>
        /// Lotação
        /// </summary>
        public string Lotacao { get; set; }

        /// <summary>
        /// DDD do telefone residencial
        /// </summary>
        public string DDDFoneResidencial { get; set; }

        /// <summary>
        /// Telefone Residencial
        /// </summary>
        public string TelefoneResidencial { get; set; }

        /// <summary>
        /// DDD do telefone celular
        /// </summary>
        public string DDDFoneCelular { get; set; }

        /// <summary>
        /// Telefone celular
        /// </summary>
        public string TelefoneCelular { get; set; }

        /// <summary>
        /// DDD Telefone Comercial
        /// </summary>
        public string DDDFoneComercial { get; set; }

        /// <summary>
        /// Telefone Comercial
        /// </summary>
        public string TelefoneComercial { get; set; }

        /// <summary>
        /// Endereco de email
        /// </summary>
        public string EnderecoDeEmail { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        public string Logradouro { get; set; }

        /// <summary>
        /// bairro
        /// </summary>
        public string Bairro { get; set; }

        /// <summary>
        /// Número
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public string Complemento { get; set; }

        /// <summary>
        /// Localidade
        /// </summary>
        public string Localidade { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        public string CEP { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        public string CPFDoParticipante { get; set; }

        /// <summary>
        /// Numero de identidade
        /// </summary>
        public string NumeroDeIdentidade { get; set; }

        /// <summary>
        /// Natureza do documento de identidade
        /// </summary>
        public string NaturezaDoDocumentoDeIdentidade { get; set; }

        /// <summary>
        /// Data de expedicao
        /// </summary>
        public string DataDeExpedicao { get; set; }

        /// <summary>
        /// Orgao Expedidor
        /// </summary>
        public string OrgaoExpedidor { get; set; }

        /// <summary>
        /// Nome do pai
        /// </summary>
        public string NomeDoPai { get; set; }

        /// <summary>
        /// Nome da mãe
        /// </summary>
        public string NomeDaMae { get; set; }

        /// <summary>
        /// Naturalidade
        /// </summary>
        public string Naturalidade { get; set; }

        /// <summary>
        /// Nacionalidade
        /// </summary>
        public string Nacionalidade { get; set; }

        /// <summary>
        /// Estado Civil
        /// </summary>
        public string EstadoCivil { get; set; }

        /// <summary>
        /// Nome do conjuge
        /// </summary>
        public string NomeDoConjuge { get; set; }

        /// <summary>
        /// Pessoa politicamente exposta
        /// </summary>
        public string PessoaPoliticamenteExposta { get; set; }

        /// <summary>
        /// Cargo
        /// </summary>
        public string Cargo { get; set; }

        /// <summary>
        /// Remuneracao na inscrição
        /// </summary>
        public string RemuneracaoNaInscricao { get; set; }

        /// <summary>
        /// Situação patrimonial
        /// </summary>
        public string SituacaoPatrimonial { get; set; }

        /// <summary>
        /// Data de admissão
        /// </summary>
        public string DataDeAdmissao { get; set; }

        /// <summary>
        /// Banco
        /// </summary>
        public string Banco { get; set; }

        /// <summary>
        /// Agencia
        /// </summary>
        public string Agencia { get; set; }

        /// <summary>
        /// Dígito Verificador da agência
        /// </summary>
        public string DigitoVerificadorAgencia { get; set; }

        /// <summary>
        /// Conta
        /// </summary>
        public string Conta { get; set; }

        /// <summary>
        /// Dígito Verificador da conta
        /// </summary>
        public string DigitoVerificadorConta { get; set; }

        /// <summary>
        /// Tipo da Conta
        /// </summary>
        public string TipoDaConta { get; set; }

        /// <summary>
        /// Fonte pagadora
        /// </summary>
        public string FontePagadora { get; set; }

        /// <summary>
        /// Pais Residencial
        /// </summary>
        public string PaisResidencial { get; set; }

        /// <summary>
        /// PIS/PASEP
        /// </summary>
        public string PISPASEP { get; set; }

        /// <summary>
        /// Data no Cargo
        /// </summary>
        public string DataNoCargo { get; set; }
	}
}
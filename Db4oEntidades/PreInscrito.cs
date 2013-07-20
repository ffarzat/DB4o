using System;

namespace Db4oEntidades
{
	[Serializable]
	public class PreInscrito : IEntidade<Guid>
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid Id { get; set; }

		/// <summary>
		/// ID do convênio de adesão
		/// </summary>
		public virtual Guid IdDoConvenioDeAdesao { get; set; }

        /// <summary>
        /// Nome do Segurado
        /// </summary>
        public virtual string NomeDoSegurado { get; set; }

        /// <summary>
        /// Data de Nascimento
        /// </summary>
        public virtual string DataDeNascimento { get; set; }

        /// <summary>
        /// Sexo do Participante
        /// </summary>
        public virtual string SexoDoParticipante { get; set; }

        /// <summary>
        /// Número de matrícula do servidor
        /// </summary>
        public virtual string NumeroDeMatriculaDoServidor { get; set; }

        /// <summary>
        /// Setor
        /// </summary>
        public virtual string Setor { get; set; }

        /// <summary>
        /// Lotação
        /// </summary>
        public virtual string Lotacao { get; set; }

        /// <summary>
        /// DDD do telefone residencial
        /// </summary>
        public virtual string DDDFoneResidencial { get; set; }

        /// <summary>
        /// Telefone Residencial
        /// </summary>
        public virtual string TelefoneResidencial { get; set; }

        /// <summary>
        /// DDD do telefone celular
        /// </summary>
        public virtual string DDDFoneCelular { get; set; }

        /// <summary>
        /// Telefone celular
        /// </summary>
        public virtual string TelefoneCelular { get; set; }

        /// <summary>
        /// DDD Telefone Comercial
        /// </summary>
        public virtual string DDDFoneComercial { get; set; }

        /// <summary>
        /// Telefone Comercial
        /// </summary>
        public virtual string TelefoneComercial { get; set; }

        /// <summary>
        /// Endereco de email
        /// </summary>
        public virtual string EnderecoDeEmail { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        public virtual string Logradouro { get; set; }

        /// <summary>
        /// bairro
        /// </summary>
        public virtual string Bairro { get; set; }

        /// <summary>
        /// Número
        /// </summary>
        public virtual string Numero { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public virtual string Complemento { get; set; }

        /// <summary>
        /// Localidade
        /// </summary>
        public virtual string Localidade { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        public virtual string CEP { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        public virtual string CPFDoParticipante { get; set; }

        /// <summary>
        /// Numero de identidade
        /// </summary>
        public virtual string NumeroDeIdentidade { get; set; }

        /// <summary>
        /// Natureza do documento de identidade
        /// </summary>
        public virtual string NaturezaDoDocumentoDeIdentidade { get; set; }

        /// <summary>
        /// Data de expedicao
        /// </summary>
        public virtual string DataDeExpedicao { get; set; }

        /// <summary>
        /// Orgao Expedidor
        /// </summary>
        public virtual string OrgaoExpedidor { get; set; }

        /// <summary>
        /// Nome do pai
        /// </summary>
        public virtual string NomeDoPai { get; set; }

        /// <summary>
        /// Nome da mãe
        /// </summary>
        public virtual string NomeDaMae { get; set; }

        /// <summary>
        /// Naturalidade
        /// </summary>
        public virtual string Naturalidade { get; set; }

        /// <summary>
        /// Nacionalidade
        /// </summary>
        public virtual string Nacionalidade { get; set; }

        /// <summary>
        /// Estado Civil
        /// </summary>
        public virtual string EstadoCivil { get; set; }

        /// <summary>
        /// Nome do conjuge
        /// </summary>
        public virtual string NomeDoConjuge { get; set; }

        /// <summary>
        /// Pessoa politicamente exposta
        /// </summary>
        public virtual string PessoaPoliticamenteExposta { get; set; }

        /// <summary>
        /// Cargo
        /// </summary>
        public virtual string Cargo { get; set; }

        /// <summary>
        /// Remuneracao na inscrição
        /// </summary>
        public virtual string RemuneracaoNaInscricao { get; set; }

        /// <summary>
        /// Situação patrimonial
        /// </summary>
        public virtual string SituacaoPatrimonial { get; set; }

        /// <summary>
        /// Data de admissão
        /// </summary>
        public virtual string DataDeAdmissao { get; set; }

        /// <summary>
        /// Banco
        /// </summary>
        public virtual string Banco { get; set; }

        /// <summary>
        /// Agencia
        /// </summary>
        public virtual string Agencia { get; set; }

        /// <summary>
        /// Dígito Verificador da agência
        /// </summary>
        public virtual string DigitoVerificadorAgencia { get; set; }

        /// <summary>
        /// Conta
        /// </summary>
        public virtual string Conta { get; set; }

        /// <summary>
        /// Dígito Verificador da conta
        /// </summary>
        public virtual string DigitoVerificadorConta { get; set; }

        /// <summary>
        /// Tipo da Conta
        /// </summary>
        public virtual string TipoDaConta { get; set; }

        /// <summary>
        /// Fonte pagadora
        /// </summary>
        public virtual string FontePagadora { get; set; }

        /// <summary>
        /// Pais Residencial
        /// </summary>
        public virtual string PaisResidencial { get; set; }

        /// <summary>
        /// PIS/PASEP
        /// </summary>
        public virtual string PISPASEP { get; set; }

        /// <summary>
        /// Data no Cargo
        /// </summary>
        public virtual string DataNoCargo { get; set; }
	}
}
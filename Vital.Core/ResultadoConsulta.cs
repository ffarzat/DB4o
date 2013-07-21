using System.Collections.Generic;

namespace Vital.Core
{
    /// <summary>
    /// Representa o retorno da páginação do CRUD Genérico
    /// </summary>
    public class ResultadoConsulta
    {
        /// <summary>
        /// Total de Páginas da consulta
        /// </summary>
        public int TotalDePaginas { get; private set; }

        /// <summary>
        /// Total de Registros da consulta
        /// </summary>
        public int TotalDeRegistros { get; private set; }

        /// <summary>
        /// Registros resultados da consulta
        /// </summary>
        public List<IEntidade> ListaRegistros { get; private set; }

        /// <summary>
        /// Cria um objeto de paginação com o resultado calcula de uma consulta
        /// </summary>
        /// <param name="totalDePaginas">Total de páginas na consulta</param>
        /// <param name="totalDeRegistros">Total de registros da consulta</param>
        /// <param name="listaRegistros">Registros resultado da consulta</param>
        public ResultadoConsulta(int totalDePaginas, int totalDeRegistros, List<IEntidade> listaRegistros)
        {
            TotalDePaginas = totalDePaginas;
            TotalDeRegistros = totalDeRegistros;
            ListaRegistros = listaRegistros;
        }

        
    }
}

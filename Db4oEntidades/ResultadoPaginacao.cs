using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db4oEntidades
{
    /// <summary>
    /// Representa o retorno da páginação do CRUD Genérico
    /// </summary>
    public class ResultadoPaginacao
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
        public List<ExpandoObject> ListaRegistros { get; private set; }

        /// <summary>
        /// Cria um objeto de paginação com o resultado calcula de uma consulta
        /// </summary>
        /// <param name="totalDePaginas">Total de páginas na consulta</param>
        /// <param name="totalDeRegistros">Total de registros da consulta</param>
        /// <param name="listaRegistros">Registros resultado da consulta</param>
        public ResultadoPaginacao(int totalDePaginas, int totalDeRegistros, List<ExpandoObject> listaRegistros)
        {
            TotalDePaginas = totalDePaginas;
            TotalDeRegistros = totalDeRegistros;
            ListaRegistros = listaRegistros;
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Vital.Core;

namespace Mongeral.Core.Servicos
{
    /// <summary>
    /// Executa as operações de descoberta de Entidades e suas validações de estado (para as operações de CRUD)
    /// </summary>
    public class ServicoDeEntidades: IServicoEntidade
    {
        /// <summary>
        /// Dada uma String descrevendo o FullName de um tipo qualquer retorna uma instância anônima que representa o mesmo
        /// </summary>
        /// <param name="tipo">FullName do Tipo</param>
        /// <returns>Instância anônima que representa o Tipo</returns>
        public IEntidade ObterAnonimoDe(string tipo)
        {
            if (tipo == null) throw new ArgumentNullException("tipo");

            Type tipoParaAnonimo = Assembly.GetExecutingAssembly().GetType(tipo);
            return Activator.CreateInstance(tipoParaAnonimo) as IEntidade;
        }

        /// <summary>
        /// Copia o estado armazendo em um ExpandoObject para um tipo anônimo de destino
        /// </summary>
        /// <param name="origem">Dados do objeto</param>
        /// <param name="destino">Tipo anônimo representando o Objeto</param>
        /// <returns>O tipo anônimo preenchido</returns>
        public IEntidade CopiarEstadoDeObjeto(ExpandoObject origem, object destino)
        {
            IDictionary<string, object> expando = origem;

            foreach (var propriedade in destino.GetType().GetProperties())
            {
                var lower = propriedade.Name.ToLower();
                var key = expando.Keys.SingleOrDefault(k => k.ToLower() == lower);

                if (key != null)
                {
                    switch (propriedade.PropertyType.Name)
                    {
                        case "Guid":
                            propriedade.SetValue(destino, Guid.Parse(expando[key].ToString()), null);
                            break;
                        default:
                            propriedade.SetValue(destino, expando[key], null);
                            break;
                    }
                }
            }

            return destino as IEntidade;
        }
    }
}

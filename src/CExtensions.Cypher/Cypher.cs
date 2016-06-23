using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CExtensions.Cypher
{
    public class Cypher : IDisposable
    {
        private IDriver Driver { get; set; }

        private ISession Session { get; set; }

        public Cypher(string url, IAuthToken token)
        {
            Driver = GraphDatabase.Driver(url, token);
            Session = Driver.Session();
        }

        public IStatementResult Match<T>(T n, params Expression<Func<T, String>>[] expressions) where T : Node
        {
            String query = "";

            foreach (var item in expressions)
            {
                MemberExpression expressionBody = (MemberExpression)item.Body;
                query += expressionBody + ",";
            }

            query = query.Remove(query.Length - 1);

            n.Key = expressions[0].Parameters[0].Name;

            //  var r =  expressionBody.Member.Name;
            query = $"MATCH {n} RETURN " + query;

            

            var result = Session.Run(query);

            return result;
        }

        

        //public IStatementResult Match<T>(Node n, Expression<Func<T, string>> expression)
        //{
        //    MemberExpression expressionBody = (MemberExpression)expression.Body;
        //    var r = expressionBody.Member.Name;

        //    var result = Session.Run($"MATCH {n} RETURN {n.Key}");

        //    return result;
        //}

        public void DeleteAll()
        {
            Session.Run("MATCH (n) Detach Delete n");
        }

        public void Create(List<Object> list)
        {
            string query = $"CREATE ";

            foreach (var item in list)
            {
                query += item.ToString() + ",";
            }

            query = query.Remove(query.Length - 1);

            Session.Run(query);

        }

        public void Dispose()
        {
            Session?.Dispose();
            Driver?.Dispose();
        }
    }
}

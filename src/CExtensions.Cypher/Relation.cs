using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CExtensions.Cypher
{
    public class Relation
    {
        public String Origin { get; set; }

        public String Destination { get; set; }

        public override string ToString()
        {
            return $"({Origin})-[:{this.GetType().Name.ToUpper()}]->({Destination})";
        }
    }
}

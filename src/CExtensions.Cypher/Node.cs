using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CExtensions.Cypher
{
    public class Node
    {
        [JsonIgnore]
        public String Key { get; set; }

        public override string ToString()
        {
            return "(" + Key + ":" + this.GetType().Name + " " + Ser(this) + ")";
        }

        public string Ser(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var writer = new JsonTextWriter(stringWriter)
                {
                    QuoteName = false,
                    QuoteChar = '\'',
                   
                    
                })
                {
                    new JsonSerializer { NullValueHandling = NullValueHandling.Ignore }.Serialize(writer, obj);
                }
                var res = stringWriter.ToString();

                return res;
            }
        }
    }
}

using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace ATM.tests.unit.Tools
{
    public abstract class ObjectDumper
    {
        public static string ToString(object o)
        {
            var stringBuilder = new StringBuilder();
            var serializer = new Serializer();
            serializer.Serialize(new IndentedTextWriter(new StringWriter(stringBuilder)), o);
            return stringBuilder.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Symmetry> rules = new List<Symmetry>();
            rules.Add(new Composite());
            rules.Add(new Symbol());

            var check = false;
            var context = new Context { Content = "caabbbaac" };
            foreach (var rule in rules)
            {
                check = check || rule.Check(context);
            }

            Console.Write(check);
            Console.Read();
        }
    }

    public abstract class Symmetry
    {
        public abstract bool Check(Context context);
    }

    public class Composite : Symmetry
    {
        public override bool Check(Context context)
        {
            if (context.Content.Length > 1)
            {
                if (context.Content[0] == context.Content[context.Content.Length - 1])
                {
                    var newContent = context.Content.Substring(1, context.Content.Length - 2); // cắt 2 đầu chuỗi
                    if (newContent != "")
                    {
                        context.Content = newContent;
                        return Check(context); // gọi đệ quy
                    }
                    else return true;
                }
                return false;
            }
            return false;
        }
    }

    public class Symbol: Symmetry
    {
        public override bool Check(Context context)
        {
            if (context.Content.Length == 1 && 
                context.Content[0] >= 'a' && context.Content[0] <= 'z')
                return true;
            return false;
        }
    }

    public class Context
    {
        public string Content { get; set; }
    }
}

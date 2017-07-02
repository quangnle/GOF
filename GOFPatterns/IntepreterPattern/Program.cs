using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntepreterPattern
{
    class Program // Client
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

    public abstract class Symmetry // AbstractExpression
    {
        public abstract bool Check(Context context);
    }

    public class Symbol : Symmetry // TerminalExpression  
    {
        public override bool Check(Context context)
        {
            if (context.Content.Length == 1 &&
                context.Content[0] >= 'a' && context.Content[0] <= 'z')
                return true;
            return false;
        }
    }

    public class Composite : Symmetry // NonterminalExpression
    {
        public override bool Check(Context context)
        {
            if (context.Content.Length > 1)
            {
                // nếu 2 ký tự ở hai đầu chuỗi không giống nhau trả về false
                if (context.Content[0] == context.Content[context.Content.Length - 1])
                {
                    var newContent = context.Content.Substring(1, context.Content.Length - 2); // cắt 2 đầu chuỗi
                    if (newContent != "") // chuỗi còn lại vẫn còn ký tự, tiếp tục parse
                    {
                        context.Content = newContent;
                        return Check(context); // gọi đệ quy cho phần content bên trong
                    }
                    else return true; // nếu hết chuỗi thì trả về true
                }
                return false;
            }
            return false;
        }
    }

    public class Context
    {
        public string Content { get; set; }
    }
}

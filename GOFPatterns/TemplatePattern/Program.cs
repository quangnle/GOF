using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplatePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var handler1 = new StdStringHandler();
            handler1.RunProcess();

            var handler2 = new FileContentHandler("poem.txt");
            handler2.RunProcess();
        }
    }

    abstract class StringLengthHandler
    {
        public abstract string PrepareString();
        public abstract void Output(int length);

        public int GetLength(string s)
        {
            // simulate more complicated logic
            Console.WriteLine("Processing '{0}'...", s);            
            return s.Length;
        }

        public void RunProcess()
        {
            var s = PrepareString();
            var length = GetLength(s);
            Output(length);
        }
    }

    class StdStringHandler : StringLengthHandler
    {
        public override void Output(int length)
        {
            Console.WriteLine("Length = {0}", length);
        }

        public override string PrepareString()
        {
            Console.Write("Enter your string: ");
            return Console.ReadLine().Trim();
        }
    }

    class FileContentHandler : StringLengthHandler
    {
        private readonly string _path;

        public FileContentHandler(string path)
        {
            _path = path;
        }

        public override void Output(int length)
        {
            File.WriteAllText(string.Format("output_{0}", _path), string.Format("{0}\n", length));
            Console.WriteLine("Saved length to output_", _path);
        }

        public override string PrepareString()
        {
            return File.ReadAllText(_path).Trim();
        }
    }
}

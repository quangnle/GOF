using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern
{
    public class Client
    {
        static void Main(string[] args)
        {
            var html = new HtmlTag("HTML");
            html.Add(new HtmlTag("Head"));

            var body = new HtmlTag("Body");
            html.Add(body);
            body.Add(new HtmlAttribute("class", "admin"));

            Console.Write(html.GetHtmlString());

            Console.ReadLine();
        }
    }

    public abstract class Element // component
    {
        protected string _name;

        // Constructor
        public Element(string name)
        {
            _name = name;
        }

        public abstract void Add(Element element);
        public abstract void Remove(Element element);

        public abstract string GetHtmlString();
    }

    public class HtmlTag : Element // composite
    {
        private List<Element> _lstElements = new List<Element>();

        public HtmlTag(string name) : base(name) { }

        public override void Add(Element element)
        {
            _lstElements.Add(element);
        }

        public override void Remove(Element element)
        {
            _lstElements.Remove(element);
        }

        public override string GetHtmlString()
        {
            var template = "<{0} {1}>{2}</{0}>";
            var attrStr = "";
            var innerStr = "";

            foreach (var element in _lstElements)
            {
                if (element is HtmlTag) innerStr += element.GetHtmlString() + "\r\n";
                else attrStr += element.GetHtmlString();
            }

            return string.Format(template, _name, attrStr, innerStr);
        }
    }

    public class HtmlAttribute : Element // leaf
    {
        private string _value = "";
        public HtmlAttribute(string name, string val)
            :base(name)
        {
            _value = val;
        }

        public override void Add(Element element)
        {
            // do nothing
        }

        public override void Remove(Element element)
        {
            // do nothing
        }

        public override string GetHtmlString()
        {
            return _name + "='" + _value + "' ";
        }
    }
}

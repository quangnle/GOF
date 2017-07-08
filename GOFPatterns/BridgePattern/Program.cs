using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePattern
{
    class Program
    {
        public static void Main(string[] agrs)
        {
            var weekdayMaker = new WeekdayMaker();
            var weekendMaker = new WeekendMaker();

            var dMenu = new DashedLineMenu();
            dMenu.Maker = weekdayMaker;

            Console.WriteLine(dMenu.CreateMenu());

            dMenu.Maker = weekendMaker;
            Console.WriteLine(dMenu.CreateMenu());

            var cMenu = new CrossedLineMenu();
            cMenu.Maker = weekendMaker;

            Console.WriteLine(cMenu.CreateMenu());
        }
    }

    public abstract class Menu // abstraction
    {
        public MenuMaker Maker { get; set; }
        public abstract string CreateMenu();
    }

    public class DashedLineMenu : Menu // refefined abstraction 
    {
        private string _separateLine = "----------";
        public override string CreateMenu()
        {
            var menu = "";
            menu += Maker.CreateHeader();
            menu += _separateLine;
            menu += Maker.CreateItemList();
            menu += _separateLine;
            menu += Maker.CreateSlogan();

            return menu;
        }
    }

    public class CrossedLineMenu : Menu // refefined abstraction 
    {
        private string _separateLine = "+++";
        public override string CreateMenu()
        {
            var menu = "";
            menu += Maker.CreateHeader();
            menu += _separateLine;
            menu += Maker.CreateItemList();
            menu += _separateLine;
            menu += Maker.CreateSlogan();

            return menu;
        }
    }

    public abstract class MenuMaker // implementor
    {
        public abstract string CreateHeader();
        public abstract string CreateItemList();
        public abstract string CreateSlogan();
    }

    public class WeekdayMaker : MenuMaker // concrete implementor
    {
        public override string CreateHeader()
        {
            return "HEADER";
        }

        public override string CreateItemList()
        {
            return "ITEM LIST";
        }

        public override string CreateSlogan()
        {
            return "SLOGAN";
        }
    }

    public class WeekendMaker : MenuMaker // concrete implementor
    {
        public override string CreateHeader()
        {
            return "*** HEADER ***";
        }

        public override string CreateItemList()
        {
            return "*** ITEM LIST ***";
        }

        public override string CreateSlogan()
        {
            return "*** SLOGAN ***";
        }
    }
}

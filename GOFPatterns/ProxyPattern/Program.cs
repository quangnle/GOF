using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern
{
    class Program
    {
        private static List<string> GetFileNames(string path)
        {
            // implement để lấy toàn bộ filename trong path
            return null;
        }

        public static List<IFileInfo> CreateFileList(string path)
        {
            List<string> fileNames = GetFileNames(path);

            if (fileNames != null && fileNames.Count > 0)
            {
                var fList = new List<IFileInfo>();
                foreach (var fname in fileNames)
                {
                    fList.Add(new FileInfoProxy { FileName = fname });
                }

                return fList;
            }

            return null;
        }

        public static void Main(string[] args)
        {
            string path = @"C:\";

            var fileList = CreateFileList(path);

            if (fileList != null)
            {
                //	in danh sách file, không cần load file
                foreach (IFileInfo f in fileList)
                {
                    Console.WriteLine(f.FileName);
                }

                // in nội dung của file đầu tiên
                fileList[0].Display();
            }

        }
    }

    public interface IFileInfo // Subject
    {
        string FileName { get; set; }
        void Display();
    }

    public class FileInfoProxy : IFileInfo // proxy
    {
        private FileInfo _fInfo; // lưu giữ biến trỏ tới real subject FileInfo

        public string FileName { get; set; }

        public void Display()
        {
            if (_fInfo == null)
                _fInfo = new FileInfo(FileName);

            _fInfo.Display();
        }
    }

    public class FileInfo : IFileInfo // real subject
    {
        public string FileName { get; set; }

        public FileInfo(string name)
        {
            FileName = name;
        }

        public void Display()
        {
            // implement để load file từ ổ cứng
            // hiển thị content của file
        }
    }
}

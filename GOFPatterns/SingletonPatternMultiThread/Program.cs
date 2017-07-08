using System;
using System.Threading;
using System.Threading.Tasks;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            MyConnection conObj = null;
            MyConnection conObj2 = null;

            Task[] tasks = new Task[2];

            tasks[0] = Task.Run(() =>
            {
                conObj = MyConnection.Instance();
            });

            tasks[1] = Task.Run(() =>
            {
                conObj2 = MyConnection.Instance();
            });

            Task.WaitAll(tasks);

            Console.WriteLine(conObj == conObj2);
        }
    }

    class MyConnection
    {
        // biến lock để đồng bộ các thread
        private static object _syncLock = new object();

        // biến static instance, khi chưa dùng được set là null
        private static MyConnection _myConnection = null;

        // singleton method
        public static MyConnection Instance()
        {
            // lưu ý là ta sử dung double-checked locking  
            // vì lệnh lock rất expensive so với check null thuần túy 
            // do đó ta check null trước
            lock (_syncLock)
            {
                // nếu instance chưa được tạo => tạo instance mới
                if (_myConnection == null)
                {
                    _myConnection = new MyConnection();
                }
            }
            
            return _myConnection;
        }
    }
}

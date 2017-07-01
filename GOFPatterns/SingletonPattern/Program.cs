using System;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var conObj = MyConnection.Instance();
            var conObj2 = MyConnection.Instance();

            Console.WriteLine(conObj == conObj2);
        }
    }

    class MyConnection
    {
        // biến static instance, khi chưa dùng được set là null
        private static MyConnection _myConnection = null;

        // singleton method
        public static MyConnection Instance()
        {
            // nếu instance chưa được tạo => tạo instance mới
            if (_myConnection == null)
            {
                _myConnection = new MyConnection();
            }
            return _myConnection;
        }
    }
}

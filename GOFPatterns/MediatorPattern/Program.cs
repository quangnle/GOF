using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var userA = new User("A");
            var userB = new User("B");
            var userC = new User("C");

            var channel = new Channel();
            channel.Register(userA);
            channel.Register(userB);

            userA.Send(userB, "Hello");
            userB.Send(userC, "Hi!");
            userB.Send(userA, "Hello!");

            Console.Read();
        }
    }

    public class User
    {
        public Channel Channel { get; set; }
        public string Name { get; private set; }

        public User(string name)
        {
            Name = name;
        }

        public void Send(User user, string msg)
        {
            Channel.Send(this, user, msg);
        }

        public void Receive(User sender, string msg)
        {
            Console.WriteLine("Received message:'{0}' from <{1}>", msg, sender.Name);
        }
    }

    public class Channel
    {
        private List<User> _users;

        public Channel()
        {
            _users = new List<User>();
        }

        public void Register(User user)
        {
            user.Channel = this;
            _users.Add(user);
        }

        public void Send(User sender, User receiver, string msg)
        {
            var exist = false;
            for (var i = 0; i < _users.Count; i++)
            {
                if (_users[i] == receiver)
                {
                    receiver.Receive(sender, msg);
                    exist = true;
                }                    
            }

            if (!exist)
                sender.Receive(new User("Server"), "Receiver is not existed in the channel.");
            
        }
    }
}

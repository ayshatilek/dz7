using System;
using System.Collections.Generic;

namespace ChatMediator
{
    public interface IMediator
    {
        void SendMessage(string message, User sender);
        void SendPrivateMessage(string message, User sender, string receiverName);
        void RegisterUser(User user);
        void RemoveUser(User user);
    }

    public class ChatRoom : IMediator
    {
        private List<User> users = new List<User>();

        public void RegisterUser(User user)
        {
            users.Add(user);
            Console.WriteLine($"{user.Name} присоединился к чату");
        }

        public void RemoveUser(User user)
        {
            users.Remove(user);
            Console.WriteLine($"{user.Name} покинул чат");
        }

        public void SendMessage(string message, User sender)
        {
            if (!users.Contains(sender))
            {
                Console.WriteLine("Ошибка: пользователь не в чате!");
                return;
            }

            foreach (var user in users)
            {
                if (user != sender)
                    user.Receive(message, sender.Name);
            }
        }

        public void SendPrivateMessage(string message, User sender, string receiverName)
        {
            var receiver = users.Find(u => u.Name == receiverName);

            if (receiver == null)
            {
                Console.WriteLine("Пользователь не найден!");
                return;
            }

            receiver.Receive("(Личное) " + message, sender.Name);
        }
    }

    public class User
    {
        public string Name { get; }
        private IMediator _mediator;

        public User(string name, IMediator mediator)
        {
            Name = name;
            _mediator = mediator;
        }

        public void Send(string message)
        {
            _mediator.SendMessage(message, this);
        }

        public void SendPrivate(string message, string receiverName)
        {
            _mediator.SendPrivateMessage(message, this, receiverName);
        }

        public void Receive(string message, string sender)
        {
            Console.WriteLine($"{sender} -> {Name}: {message}");
        }
    }

    class Program
    {
        static void Main()
        {
            IMediator chat = new ChatRoom();

            var user1 = new User("Алиса", chat);
            var user2 = new User("Боб", chat);
            var user3 = new User("Чарли", chat);

            chat.RegisterUser(user1);
            chat.RegisterUser(user2);
            chat.RegisterUser(user3);

            user1.Send("Привет всем!");
            user2.SendPrivate("Привет, Алиса!", "Алиса");

            chat.RemoveUser(user3);
            user3.Send("Меня слышно?"); // ошибка
        }
    }
}

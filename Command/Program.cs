using System;
using System.Collections.Generic;

namespace SmartHomeCommand
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class Light
    {
        public void On() => Console.WriteLine("Свет включен");
        public void Off() => Console.WriteLine("Свет выключен");
    }

    public class Door
    {
        public void Open() => Console.WriteLine("Дверь открыта");
        public void Close() => Console.WriteLine("Дверь закрыта");
    }

    public class Thermostat
    {
        private int temperature = 20;

        public void Increase()
        {
            temperature++;
            Console.WriteLine($"Температура увеличена до {temperature}");
        }

        public void Decrease()
        {
            temperature--;
            Console.WriteLine($"Температура уменьшена до {temperature}");
        }
    }

    public class TV
    {
        public void On() => Console.WriteLine("Телевизор включен");
        public void Off() => Console.WriteLine("Телевизор выключен");
    }

    public class LightOnCommand : ICommand
    {
        private Light _light;
        public LightOnCommand(Light light) => _light = light;
        public void Execute() => _light.On();
        public void Undo() => _light.Off();
    }

    public class LightOffCommand : ICommand
    {
        private Light _light;
        public LightOffCommand(Light light) => _light = light;
        public void Execute() => _light.Off();
        public void Undo() => _light.On();
    }

    public class DoorOpenCommand : ICommand
    {
        private Door _door;
        public DoorOpenCommand(Door door) => _door = door;
        public void Execute() => _door.Open();
        public void Undo() => _door.Close();
    }

    public class ThermostatIncreaseCommand : ICommand
    {
        private Thermostat _thermostat;
        public ThermostatIncreaseCommand(Thermostat thermostat) => _thermostat = thermostat;
        public void Execute() => _thermostat.Increase();
        public void Undo() => _thermostat.Decrease();
    }

    public class TVOnCommand : ICommand
    {
        private TV _tv;
        public TVOnCommand(TV tv) => _tv = tv;
        public void Execute() => _tv.On();
        public void Undo() => _tv.Off();
    }
    
    public class RemoteControl
    {
        private Stack<ICommand> history = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            history.Push(command);
        }

        public void UndoLast()
        {
            if (history.Count == 0)
            {
                Console.WriteLine("Нет команд для отмены!");
                return;
            }

            ICommand command = history.Pop();
            command.Undo();
        }
    }

    class Program
    {
        static void Main()
        {
            var light = new Light();
            var door = new Door();
            var thermostat = new Thermostat();
            var tv = new TV();

            var remote = new RemoteControl();

            remote.ExecuteCommand(new LightOnCommand(light));
            remote.ExecuteCommand(new DoorOpenCommand(door));
            remote.ExecuteCommand(new ThermostatIncreaseCommand(thermostat));
            remote.ExecuteCommand(new TVOnCommand(tv));

            Console.WriteLine("\nОтмена команд:");
            remote.UndoLast();
            remote.UndoLast();
            remote.UndoLast();
            remote.UndoLast();
            remote.UndoLast(); // ошибка
        }
    }
}

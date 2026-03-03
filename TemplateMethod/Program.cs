using System;

namespace BeverageTemplate
{
    public abstract class Beverage
    {
        public void PrepareRecipe()
        {
            BoilWater();
            Brew();
            PourInCup();
            if (CustomerWantsCondiments())
                AddCondiments();
        }

        protected void BoilWater() => Console.WriteLine("Кипятим воду");
        protected void PourInCup() => Console.WriteLine("Наливаем в чашку");

        protected abstract void Brew();
        protected abstract void AddCondiments();

        protected virtual bool CustomerWantsCondiments()
        {
            Console.Write("Добавить добавки? (y/n): ");
            string input = Console.ReadLine()?.ToLower();

            if (input == "y") return true;
            if (input == "n") return false;

            Console.WriteLine("Некорректный ввод. По умолчанию: нет.");
            return false;
        }
    }

    public class Tea : Beverage
    {
        protected override void Brew() =>
            Console.WriteLine("Завариваем чай");

        protected override void AddCondiments() =>
            Console.WriteLine("Добавляем лимон");
    }

    public class Coffee : Beverage
    {
        protected override void Brew() =>
            Console.WriteLine("Завариваем кофе");

        protected override void AddCondiments() =>
            Console.WriteLine("Добавляем сахар и молоко");
    }

    public class HotChocolate : Beverage
    {
        protected override void Brew() =>
            Console.WriteLine("Растворяем какао порошок");

        protected override void AddCondiments() =>
            Console.WriteLine("Добавляем маршмеллоу");
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Готовим чай:");
            Beverage tea = new Tea();
            tea.PrepareRecipe();

            Console.WriteLine("\nГотовим кофе:");
            Beverage coffee = new Coffee();
            coffee.PrepareRecipe();

            Console.WriteLine("\nГотовим горячий шоколад:");
            Beverage chocolate = new HotChocolate();
            chocolate.PrepareRecipe();
        }
    }
}

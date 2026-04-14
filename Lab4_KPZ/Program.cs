using System;

namespace task1
{
    abstract class SupportHandler
    {
        protected SupportHandler Successor;

        public void SetSuccessor(SupportHandler successor)
        {
            this.Successor = successor;
        }

        public abstract void HandleRequest(int choice);
    }

    class GeneralSupport : SupportHandler
    {
        public override void HandleRequest(int choice)
        {
            if (choice == 1)
            {
                Console.WriteLine("Баланс: 100 грн.");
            }
            else if (Successor != null)
            {
                Successor.HandleRequest(choice);
            }
        }
    }

    class InternetSupport : SupportHandler
    {
        public override void HandleRequest(int choice)
        {
            if (choice == 2)
            {
                Console.WriteLine("Налаштування інтернету надіслано.");
            }
            else if (Successor != null)
            {
                Successor.HandleRequest(choice);
            }
        }
    }

    class BillingSupport : SupportHandler
    {
        public override void HandleRequest(int choice)
        {
            if (choice == 3)
            {
                Console.WriteLine("З'єдную з оператором...");
            }
            else if (Successor != null)
            {
                Successor.HandleRequest(choice);
            }
        }
    }

    class TechExpertSupport : SupportHandler
    {
        public override void HandleRequest(int choice)
        {
            if (choice == 4)
            {
                Console.WriteLine("Заявка майстру прийнята.");
            }
            else
            {
                Console.WriteLine("Помилка! Спробуйте ще раз.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            SupportHandler h1 = new GeneralSupport();
            SupportHandler h2 = new InternetSupport();
            SupportHandler h3 = new BillingSupport();
            SupportHandler h4 = new TechExpertSupport();

            h1.SetSuccessor(h2);
            h2.SetSuccessor(h3);
            h3.SetSuccessor(h4);

            bool finished = false;

            while (!finished)
            {
                Console.WriteLine("\nМеню");
                Console.WriteLine("1. Баланс");
                Console.WriteLine("2. Інтернет");
                Console.WriteLine("3. Оплата");
                Console.WriteLine("4. Технік");
                Console.Write("Вибір: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice >= 1 && choice <= 4)
                    {
                        h1.HandleRequest(choice);
                        finished = true;
                    }
                    else
                    {
                        h1.HandleRequest(choice);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
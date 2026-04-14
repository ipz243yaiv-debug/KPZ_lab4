using System;

namespace task4
{
    interface IImageLoadStrategy
    {
        void Load(string href);
    }
    class FileImageStrategy : IImageLoadStrategy
    {
        public void Load(string href)
        {
            Console.WriteLine($"Завантаження картинки з диска: {href}");
        }
    }

    class NetworkImageStrategy : IImageLoadStrategy
    {
        public void Load(string href)
        {
            Console.WriteLine($"Завантаження картинки з мережі: {href}");
        }
    }
    class LightImage
    {
        private IImageLoadStrategy _strategy;
        private string _href;

        public LightImage(string href)
        {
            _href = href;

            if (href.StartsWith("http"))
            {
                _strategy = new NetworkImageStrategy();
            }
            else
            {
                _strategy = new FileImageStrategy();
            }
        }

        public void Display()
        {
            _strategy.Load(_href);
            Console.WriteLine($"<img src='{_href}' />");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Тест 1");
            LightImage webImg = new LightImage("https://google.com/logo.png");
            webImg.Display();

            Console.WriteLine("\nТест 2");
            LightImage localImg = new LightImage("C:/Users/Admin/photo.jpg");
            localImg.Display();

            Console.ReadKey();
        }
    }
}
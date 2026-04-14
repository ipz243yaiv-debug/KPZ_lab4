using System;
using System.Collections.Generic;
using System.Text;

namespace task3
{
    interface ILightNode
    {
        string Render(string content);
    }

    class LightElementFlyweight : ILightNode
    {
        public string TagName { get; }

        private Dictionary<string, List<Action>> _events = new Dictionary<string, List<Action>>();

        public LightElementFlyweight(string tagName) => TagName = tagName;

        public void AddEventListener(string eventType, Action callback)
        {
            if (!_events.ContainsKey(eventType))
            {
                _events[eventType] = new List<Action>();
            }
            _events[eventType].Add(callback);
        }

        public void TriggerEvent(string eventType)
        {
            if (_events.ContainsKey(eventType))
            {
                foreach (var action in _events[eventType])
                {
                    action.Invoke();
                }
            }
        }

        public string Render(string content) => $"<{TagName}>{content}</{TagName}>";
    }

    class FlyweightFactory
    {
        private Dictionary<string, LightElementFlyweight> _tags = new Dictionary<string, LightElementFlyweight>();

        public LightElementFlyweight GetTag(string tagName)
        {
            if (!_tags.ContainsKey(tagName))
                _tags[tagName] = new LightElementFlyweight(tagName);
            return _tags[tagName];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            FlyweightFactory factory = new FlyweightFactory();

            var button = factory.GetTag("button");
            var div = factory.GetTag("div");

            button.AddEventListener("click", () => Console.WriteLine("Лог: Кнопку клікнули"));
            button.AddEventListener("mouseover", () => Console.WriteLine("Лог: Мишка пройшла над кнопкою"));

            div.AddEventListener("click", () => Console.WriteLine("Лог: Клік по блоку DIV"));

            Console.WriteLine("Симуляція подій");

            Console.WriteLine("Користувач тисне на кнопку:");
            button.TriggerEvent("click");

            Console.WriteLine("\nКористувач навів мишку на кнопку:");
            button.TriggerEvent("mouseover");

            Console.WriteLine("\nКористувач тисне на DIV:");
            div.TriggerEvent("click");

            Console.ReadKey();
        }
    }
}
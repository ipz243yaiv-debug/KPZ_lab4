using System;
using System.Collections.Generic;

namespace task5
{
    class TextDocumentMemento
    {
        public string Content { get; }
        public DateTime Date { get; }

        public TextDocumentMemento(string content)
        {
            Content = content;
            Date = DateTime.Now;
        }
    }
    class TextDocument
    {
        private string _content;

        public void SetContent(string text)
        {
            _content = text;
        }

        public void Show()
        {
            Console.WriteLine($"Текст: {_content}");
        }

        public TextDocumentMemento CreateMemento()
        {
            return new TextDocumentMemento(_content);
        }

        public void Restore(TextDocumentMemento memento)
        {
            _content = memento.Content;
        }
    }

    class TextEditor
    {
        private List<TextDocumentMemento> _history = new List<TextDocumentMemento>();
        private TextDocument _document;

        public TextEditor(TextDocument doc)
        {
            _document = doc;
        }

        public void Backup()
        {
            _history.Add(_document.CreateMemento());
        }

        public void Undo()
        {
            if (_history.Count == 0)
            {
                Console.WriteLine("Історія порожня!");
                return;
            }

            var memento = _history[_history.Count - 1];
            _history.RemoveAt(_history.Count - 1);

            _document.Restore(memento);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            TextDocument doc = new TextDocument();
            TextEditor editor = new TextEditor(doc);

            doc.SetContent("Це перша версія");
            doc.Show();
            editor.Backup(); 

            doc.SetContent("Додав тексту");
            doc.Show();
            editor.Backup(); 

            doc.SetContent("Цей текст буде видалено через Undo");
            doc.Show();

            Console.WriteLine("\nВідміна");
            editor.Undo();
            doc.Show();

            Console.WriteLine("\nЩе одна відміна");
            editor.Undo();
            doc.Show();

            Console.ReadKey();
        }
    }
}
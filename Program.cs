using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
// Класс, представляющий книгу
public class Book
{
    public string Title { get; }
    public string Author { get; }
    public int Year { get; }
    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }
    public override string ToString()
    {
        return $"{Title} by {Author} ({Year})";
    }
}
// Класс, представляющий библиотеку
public class Library : IEnumerable<Book>
{
    private readonly List<Book> _books = new List<Book>();
    public void AddBook(Book book)
    {
        _books.Add(book);
    }
    public IEnumerator<Book> GetEnumerator()
    {
        return new LibraryEnumerator(_books);
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    // Вложенный класс для итератора
    private class LibraryEnumerator : IEnumerator<Book>
    {
        private readonly List<Book> _books;
        private int _position = -1;
        public LibraryEnumerator(List<Book> books)
        {
            _books = books;
        }
        public Book Current => _books[_position];
        object IEnumerator.Current => Current;
        public bool MoveNext()
        {
            _position++;
            return _position < _books.Count;
        }
        public void Reset()
        {
            _position = -1;
        }
        public void Dispose()
        {
            // Здесь можно освободить ресурсы, если это необходимо
        }
    }
}
// Тестовая программа
public class Program
{
    public static void Main(string[] args)
    {
        var library = new Library();
        // Добавление книг в библиотеку
        library.AddBook(new Book("Война и мир", "Лев Толстой", 1869));
        library.AddBook(new Book("Преступление и наказание", "Фёдор Достоевский", 1866));
        library.AddBook(new Book("Анна Каренина", "Лев Толстой", 1877));
        library.AddBook(new Book("Мастер и Маргарита", "Михаил Булгаков", 1967));
        library.AddBook(new Book("Тихий Дон", "Михаил Шолохов", 1928));
        // Перебор книг в библиотеке с использованием итератора
        foreach (var book in library)
        {
            Console.WriteLine(book);
        }
    }
}
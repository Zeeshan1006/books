using Books.Api.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Books.Api.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(b => b.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book book) =>
            _books.ReplaceOne(b => b.Id == id, book);

        public void Remove(Book book) =>
            _books.DeleteOne(b => b.Id == book.Id);

        public void Remove(string id) =>
            _books.DeleteOne(b => b.Id == id);
    }
}

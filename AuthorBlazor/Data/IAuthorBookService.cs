using AuthorBlazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorBlazor.Data
{
    public interface IAuthorBookService
    {
        Task<IList<Book>> GetBooksAsync();
        Task<IList<Author>> GetAuthorsAsync();
        Task RemoveBookAsync(int Id);
        Task AddBookAsync(Book book, int id);
        Task AddAuthorAsync(Author author);

    }
}

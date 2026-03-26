using WebApi.Data.DTO;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IBookService
    {


        Task<List<BookDto>> GetAll();

        Task<BookDto> GetById(long id);

        Task<BookDto> Update(BookDto book);

        Task<BookDto> Create(BookDto book);

        Task DeleteBook(long id);
    }
}

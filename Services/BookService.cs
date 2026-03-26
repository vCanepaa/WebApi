using Mapster;
using WebApi.Data.DTO;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        public BookService(IRepository<Book> repository)
        {
             _repository = repository;
        }

        public async Task<BookDto> Create(BookDto book)
        {

            var entity = book.Adapt<Book>();
            entity = await _repository.Create(entity);
            return entity.Adapt<BookDto>();
        }

        public async Task DeleteBook(long id)
        {
             await _repository.Delete(id);
        }

        public async Task<List<BookDto>> GetAll()
        {
            var entityList = await _repository.GetAll();

            return entityList.Adapt<List<BookDto>>();
        }

        public async Task<BookDto> GetById(long id)
        {
            var entity = await _repository.GetById(id);

            return entity.Adapt<BookDto>();
        }

        public async Task<BookDto> Update(BookDto book)
        {
            var entity = book.Adapt<Book>();
            entity = await _repository.Update(entity);

            return entity.Adapt<BookDto>();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebApi.Data.DTO;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private IBookService _bookService;
        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet("{id:long}")]

        public async Task<IActionResult> Get([FromRoute] long id)
        {
            _logger.LogInformation("Fetching Book with ID: {id}", id);

            var bookInDb = await _bookService.GetById(id);

            if (bookInDb == null)
            {
                _logger.LogWarning("Book not found");

                return NotFound("Book not found");
            }

            _logger.LogDebug("{id} Book Returned", id);

            return Ok(bookInDb);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Fetching all persons");
            var booksInDb = await _bookService.GetAll();

            if (booksInDb == null || !booksInDb.Any())
            {
                _logger.LogWarning("Books not found");

                return NotFound("There is no book in database");
            }

            return Ok(booksInDb);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] BookDto book)
        {
            var personInDb = await _bookService.GetById(book.Id);
            if (personInDb == null)
            {
                await _bookService.Create(book);
                _logger.LogDebug("{id} Book Created", book.Id);

                return Ok(book);
            }


            return BadRequest("Book Already Exist");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromBody] BookDto book)
        {

            _logger.LogInformation("Update Book with ID{id}", book.Id);

            var personInDb = await _bookService.GetById(book.Id);
            if (personInDb != null)
            {
                await _bookService.Update(book);
                _logger.LogDebug("{id} Book updated", book.Id);

                return Ok(book);
            }
            _logger.LogError("{id} Id not Found", book.Id);

            return NotFound("Book not found");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(long id)
        {
            var personInDb = await _bookService.GetById(id);
            if (personInDb != null)
            {
                await _bookService.DeleteBook(id);
                return Ok("Deletado Com sucesso");
            }
            _logger.LogWarning("{id} Id not Found", id);

            return NotFound("Pessoa não existe");
        }
    }
}

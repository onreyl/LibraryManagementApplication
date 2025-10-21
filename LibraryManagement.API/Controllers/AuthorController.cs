using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound($"Author with ID {id} not found");
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthor(AuthorDto authorDto)
        {
            var author = await _authorService.CreateAuthorAsync(authorDto);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> UpdateAuthor(int id, AuthorDto authorDto)
        {
            authorDto.Id = id;
            var author = await _authorService.UpdateAuthorAsync(authorDto);
            return Ok(author);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthorAsync(id);
            if (!result)
                return BadRequest("Author cannot be deleted. It has associated books or does not exist");
            return NoContent();
        }

        [HttpGet("with-books")]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorsWithBooks()
        {
            var authors = await _authorService.GetAuthorsWithBooksAsync();
            return Ok(authors);
        }
    }
}

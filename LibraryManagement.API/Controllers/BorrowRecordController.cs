using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowRecordController : ControllerBase
    {
        private readonly IBorrowRecordService _borrowRecordService;

        public BorrowRecordController(IBorrowRecordService borrowRecordService)
        {
            _borrowRecordService = borrowRecordService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowRecordDto>>> GetAllBorrowRecords()
        {
            var records = await _borrowRecordService.GetAllBorrowRecordsAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowRecordDto>> GetBorrowRecord(int id)
        {
            var record = await _borrowRecordService.GetBorrowRecordByIdAsync(id);
            if (record == null)
                return NotFound($"Borrow record with ID {id} not found");
            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<BorrowRecordDto>> CreateBorrowRecord(BorrowRecordDto borrowRecordDto)
        {
            var record = await _borrowRecordService.CreateBorrowRecordAsync(borrowRecordDto);
            return CreatedAtAction(nameof(GetBorrowRecord), new { id = record.Id }, record);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BorrowRecordDto>> UpdateBorrowRecord(int id, BorrowRecordDto borrowRecordDto)
        {
            borrowRecordDto.Id = id;
            var record = await _borrowRecordService.UpdateBorrowRecordAsync(borrowRecordDto);
            return Ok(record);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBorrowRecord(int id)
        {
            var result = await _borrowRecordService.DeleteBorrowRecordAsync(id);
            if (!result)
                return BadRequest("Borrow record cannot be deleted or does not exist");
            return NoContent();
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<BorrowRecordDto>>> GetActiveBorrows()
        {
            var records = await _borrowRecordService.GetActiveBorrowsAsync();
            return Ok(records);
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<BorrowRecordDto>>> GetOverdueBorrows()
        {
            var records = await _borrowRecordService.GetOverdueBorrowsAsync();
            return Ok(records);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BorrowRecordDto>>> GetBorrowsByUser(int userId)
        {
            var records = await _borrowRecordService.GetBorrowsByUserAsync(userId);
            return Ok(records);
        }

        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<IEnumerable<BorrowRecordDto>>> GetBorrowsByBook(int bookId)
        {
            var records = await _borrowRecordService.GetBorrowsByBookAsync(bookId);
            return Ok(records);
        }

        [HttpPost("{id}/return")]
        public async Task<ActionResult> ReturnBook(int id)
        {
            var result = await _borrowRecordService.ReturnBookAsync(id);
            if (!result)
                return BadRequest("Book cannot be returned or record does not exist");
            return NoContent();
        }
    }
}

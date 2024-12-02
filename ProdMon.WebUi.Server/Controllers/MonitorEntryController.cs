using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using ProdMon.Domain.Models;

namespace ProdMon.WebUi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorEntryController : ControllerBase
    {
        private readonly IEntryRepository _entryRepository;

        public MonitorEntryController(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        // GET: api/MonitorEntry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonitorEntry>>> GetMonitorEntries()
        {
            var entries = await _entryRepository.GetAllEntriesAsync();
            return Ok(entries);
        }

        // GET: api/MonitorEntry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonitorEntry>> GetMonitorEntry(string id)
        {
            var entry = await _entryRepository.GetEntryByIdAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        // POST: api/MonitorEntry
        [HttpPost]
        public async Task<ActionResult<MonitorEntry>> PostMonitorEntry(MonitorEntry entry)
        {
            await _entryRepository.AddEntryAsync(entry);
            return CreatedAtAction(nameof(GetMonitorEntry), new { id = entry.Dmc }, entry);
        }

        // PUT: api/MonitorEntry/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonitorEntry(string id, MonitorEntry entry)
        {
            if (id != entry.Dmc)
            {
                return BadRequest();
            }

            await _entryRepository.UpdateEntryAsync(entry);
            return NoContent();
        }

        // DELETE: api/MonitorEntry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonitorEntry(string id)
        {
            await _entryRepository.DeleteEntryAsync(id);
            return NoContent();
        }
    }
}

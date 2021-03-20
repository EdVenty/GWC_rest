using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GWC_rest.Data;
using GWC_rest.Models;
using Microsoft.AspNetCore.Authorization;

namespace GWC_rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly ConversationContext _context;

        public ConversationsController(ConversationContext context)
        {
            _context = context;
        }

        // GET: api/Conversations
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversation()
        {
            return await _context.Conversation.ToListAsync();
        }

        // GET: api/Conversations/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Conversation>> GetConversation(int id)
        {
            var conversation = await _context.Conversation.FindAsync(id);

            if (conversation == null)
            {
                return NotFound();
            }

            return conversation;
        }

        // PUT: api/Conversations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutConversation(int id, Conversation conversation)
        {
            if (id != conversation.Id)
            {
                return BadRequest();
            }

            _context.Entry(conversation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Conversations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Conversation>> PostConversation(Conversation conversation)
        {
            _context.Conversation.Add(conversation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConversation", new { id = conversation.Id }, conversation);
        }

        // DELETE: api/Conversations/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteConversation(int id)
        {
            var conversation = await _context.Conversation.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }

            _context.Conversation.Remove(conversation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversation.Any(e => e.Id == id);
        }
    }
}

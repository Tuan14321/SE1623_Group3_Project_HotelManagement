using Hotel_Management_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly DataHotelContext _context;

        public InvoicesController(DataHotelContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }
            return await _context.Invoices.ToListAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public IActionResult GetInvoiceById(int id)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }
            List<Invoice> invoice = _context.Invoices.Where(i => i.InvoiceId == id)
                                                     .Include(c => c.Customer)
                                                     .Include(u => u.User)
                                                     .Include(s => s.Services)
                                                     .Include(i => i.Room)
                                                        .ThenInclude(r => r.Type)
                                                     .Include(i => i.Room)
                                                        .ThenInclude(r => r.Status)
                                                     .Include(i => i.Room)
                                                        .ThenInclude(r => r.Floor)
                                                     .ToList();
            if (invoice == null)
            {
                return NotFound("Ko có");
            }

            List<InvoiceDTO> invoicesDTO = new List<InvoiceDTO>();

            if (!invoicesDTO.Any())
            {
                invoicesDTO = new List<InvoiceDTO>();
            }
            foreach (var item in invoice)
            {
                InvoiceDTO invoices = new InvoiceDTO();
                invoices.InvoiceId = item.InvoiceId;
                if (item.Room != null)
                {
                    invoices.TypeName = item.Room.Type.Name;
                    invoices.Status = item.Room.Status.StatusName;
                    invoices.Floor = item.Room.Floor.FloorName;
                }
                invoices.CustomerName = item.Customer.LastName;
                invoices.UserName = item.User.UserName;
                invoices.CheckInTime = item.CheckInTime;
                invoices.CheckOutTime = item.CheckOutTime;
                if (((TimeSpan)(item.CheckOutTime - item.CheckInTime)).TotalHours <= 24)
                {
                    invoices.TotalPrice = (item.Room.Type.PricePerHour * (((TimeSpan)(item.CheckOutTime - item.CheckInTime)).TotalHours)) + item.Services.Sum(s => s.Price);
                }
                else
                {
                    invoices.TotalPrice = (item.Room.Type.PricePerHour * (((TimeSpan)(item.CheckOutTime - item.CheckInTime)).TotalHours)) + item.Services.Sum(s => s.Price);
                }
                invoicesDTO.Add(invoices);
            }
            return Ok(invoicesDTO);
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'DataHotelContext.Invoices'  is null.");
            }
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.InvoiceId }, invoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(int id)
        {
            return (_context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}

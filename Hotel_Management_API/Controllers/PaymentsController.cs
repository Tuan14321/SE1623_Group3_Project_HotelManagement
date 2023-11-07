using Hotel_Management_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly DataHotelContext _context;

        public PaymentsController(DataHotelContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            if (_context.Payments == null)
            {
                return NotFound();
            }
            return await _context.Payments.ToListAsync();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            if (_context.Payments == null)
            {
                return NotFound();
            }
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            if (_context.Payments == null)
            {
                return Problem("Entity set 'DataHotelContext.Payments'  is null.");
            }
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayment", new { id = payment.PaymentId }, payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (_context.Payments == null)
            {
                return NotFound();
            }
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return (_context.Payments?.Any(e => e.PaymentId == id)).GetValueOrDefault();
        }
        [HttpPost("pay/{invoiceId}")]
        public async Task<IActionResult> MakePayment(int invoiceId, [FromBody] Payment paymentRequest)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);

            if (invoice == null)
            {
                return NotFound("Hóa đơn không tồn tại.");
            }

            // Tính tổng tiền từ hóa đơn
            double totalPrice = (double)CalculateTotalPrice(invoice);

            // Kiểm tra xem tổng tiền thanh toán từ người dùng có khớp với tổng tiền từ hóa đơn
            if (totalPrice != paymentRequest.Amount)
            {
                return BadRequest("Số tiền thanh toán không khớp với tổng tiền hóa đơn.");
            }

            // Tạo đối tượng Payment
            var payment = new Payment
            {
                InvoiceId = invoice.InvoiceId,
                Amount = paymentRequest.Amount,
                PaymentDate = DateTime.Now,  // Thời gian thanh toán
                PaymentMethod = paymentRequest.PaymentMethod  // Phương thức thanh toán từ người dùng
            };

            // Lưu đối tượng Payment vào cơ sở dữ liệu
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok("Thanh toán thành công.");
        }

        private decimal CalculateTotalPrice(Invoice invoice)
        {
            // Điều này giả định rằng bạn đã tính toán tổng tiền theo logic của bạn, ví dụ: phí phòng, phí dịch vụ, v.v.
            return (decimal)invoice.TotalPrice;
        }

    }
}

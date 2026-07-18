using Microsoft.AspNetCore.Mvc;
using OMS.Api.DTOs;
using OMS.Model;
using OMS.Services.Interfaces;

namespace OMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public ActionResult<List<Payment>> GetAll()
        {
            var payments = _paymentService.GetAllPayments();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public ActionResult<Payment> GetById(int id)
        {
            var payment = _paymentService.GetPaymentById(id);

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        [HttpGet("order/{orderId}")]
        public ActionResult<List<Payment>> GetByOrderId(int orderId)
        {
            var payments = _paymentService.GetPaymentsByOrderId(orderId);
            return Ok(payments);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreatePaymentRequest request)
        {
            try
            {
                var payment = new PaymentBuilder()
                    .ForOrder(request.OrderId)
                    .WithAmount(request.Amount)
                    .WithPaymentMethod(request.PaymentMethod)
                    .WithStatus(PaymentStatus.Pending)
                    .WithTransactionReference(request.TransactionReference ?? string.Empty)
                    .WithNotes(request.Notes ?? string.Empty)
                    .Build();

                _paymentService.CreatePayment(payment);

                return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Payment payment)
        {
            if (id != payment.Id)
                return BadRequest("Route id and payment id must match.");

            try
            {
                _paymentService.UpdatePayment(payment);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _paymentService.DeletePayment(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
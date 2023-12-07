using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using Microsoft.Extensions.Options;

namespace CleaningServicesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutApiController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CheckoutApiController> _logger;

        public CheckoutApiController(ILogger<CheckoutApiController> logger)
        {
            _logger = logger;
        }

      
        [HttpPost]
        public ActionResult GetStripeSecret(string prodId)
        {
            var domain = "https://localhost:7290/";
            var options = new SessionCreateOptions
            {
                
                UiMode = "embedded",
            LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = prodId,//"price_1OJpB6EU0SXAQ8qqzREa4mTN",
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                ReturnUrl = domain + "Home/Confirmation?session_id={CHECKOUT_SESSION_ID}"
            };
        var service = new SessionService();
        Session session = service.Create(options);

            return new JsonResult(new {clientSecret = session.RawJObject["client_secret"]
    });
        }

      
    }
}

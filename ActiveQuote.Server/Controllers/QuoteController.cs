using Microsoft.AspNetCore.Mvc;

namespace ActiveQuote.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {

        private readonly QuoteContext _context;
        public QuoteController(QuoteContext context)
        {
            _context = context;
        }

        // List of insurer names
        private static readonly List<string> Insurers = new List<string>
        {
            "Alpha Insurance",
            "Beta Insurance",
            "Gamma Insurance",
            "Delta Insurance",
            "Epsilon Insurance"
        };

        // get a random insurer's name
        private string GetRandomInsurer()
        {
            var random = new Random();
            int index = random.Next(Insurers.Count);
            return Insurers[index];
        }

        // get a random cost per month (10 to 100)
        private int GetRandomCostPerMonth()
        {
            var random = new Random();
            return random.Next(10, 101); // Returns a number between 10 and 100
        }

        // get a random length of policy (1 to 50 years)
        private int GetRandomLengthOfPolicy()
        {
            var random = new Random();
            return random.Next(1, 51); // Returns a number between 1 and 50
        }

        // POST endpoint to calculate and return a Quote object
        [HttpPost("submit-quote")]
        public IActionResult SubmitQuote([FromBody] FormData formData)
        {
            try
            {
                // Validate the incoming data (basic validation)
                if (string.IsNullOrEmpty(formData.FirstName) || string.IsNullOrEmpty(formData.LastName) ||
                    string.IsNullOrEmpty(formData.Dob) || string.IsNullOrEmpty(formData.Email) ||
                    string.IsNullOrEmpty(formData.Phone))
                {
                    return BadRequest("All fields are required.");
                }

                // Calculate the quote
                var quote = new Quote
                {
                    InsurersName = GetRandomInsurer(),
                    CostPerMonth = GetRandomCostPerMonth(),
                    LengthOfPolicy = GetRandomLengthOfPolicy()
                };

                // Return the calculated quote
                return Ok(quote);
                // Save the quote to the database
                _context.Quotes.Add(quote);

                // Save the changes to the database
                _context.SaveChanges(); 
            }

            catch (Exception ex)
            {
                return BadRequest( ex);
            }
        }
    }
}

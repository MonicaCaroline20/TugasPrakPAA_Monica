using Microsoft.AspNetCore.Mvc;
using Monica_Caroline.Models;

namespace Monica_Caroline.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("api/person")]
        public ActionResult<Person> ListPerson()
        {
            PersonContext context = new PersonContext();
            List<Person> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }
        [HttpGet("api/person/{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            PersonContext context = new PersonContext();
            {
                var person = context.ListPerson().Find(p => p.id_person == id);
                if (person == null)
                {
                    return NotFound();
                }

                return Ok(person);
            }
        }
    }
}

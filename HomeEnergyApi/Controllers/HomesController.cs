using Microsoft.AspNetCore.Mvc;

namespace HomeEnergyUsageApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomesController : ControllerBase
    {
        private static List<Home> homesList = new List<Home>();

        [HttpGet]
        public IEnumerable<Home> Get()
        {
            return homesList;
        }

        [HttpGet("{ownerLastName}")]
        public Home FindByOwnerLastName(string ownerLastName)
        {
            foreach (Home home in homesList)
            {
                if (home.ownerLastName == ownerLastName)
                {
                    return home;
                }
            }
            return null;
        }

        [HttpPost]
        public Home Post([FromBody] Home home)
        {
            homesList.Add(home);
            return home;
        }
    }
}

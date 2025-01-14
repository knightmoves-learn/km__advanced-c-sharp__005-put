using Microsoft.AspNetCore.Mvc;

namespace HomeEnergyUsageApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomesController : ControllerBase
    {
        private static List<Home> homes = new List<Home>();
        private Home notFound = new Home(0, "Nobody", "000 Nowhere Ave", "No Owner Was Found", 0);

        [HttpGet]
        public IEnumerable<Home> Get()
        {
            return homes;
        }

        [HttpGet("{ownerLastName}")]
        public Home FindByOwnerLastName(string ownerLastName)
        {
            foreach (Home home in homes)
            {
                if (home.ownerLastName == ownerLastName)
                {
                    return home;
                }
            }
            return notFound;
        }

        [HttpPost]
        public Home Post([FromBody] Home home)
        {
            homes.Add(home);
            return home;
        }

        //start
        [HttpPut("{ownerLastName}")]
        public Home UpdateHome([FromBody] Home newHome, [FromRoute] string ownerLastName)
        {
            for (int i = 0; i < homes.Count; i++)
            {
                if (homes[i].ownerLastName == ownerLastName)
                {
                    homes[i] = newHome;
                    return newHome;
                }
            }
            return notFound;
        }
        //end
    }

}

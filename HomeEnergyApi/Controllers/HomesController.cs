using Microsoft.AspNetCore.Mvc;

namespace HomeEnergyUsageApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomesController : ControllerBase
    {
        // private static List<Home> homes = new List<Home>() {
        //     new Home(1, "Kim", "204 Maple Hill Road", "Atlanta", 4923),
        //     new Home(2, "Garcia", "West 7th Street", "Tuscon", 3521),
        //     new Home(3, "Connor", "332 Birchwood Circle", "Miami", 2576)};
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

using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers
{
    public class ParkMapper
    {
        public static Park DLtoBL(ParkEF park)
        {
                    if (park == null)
        {
            return null;
        }
        Park Park = new Park(park.Id, park.Naam, park.Location);

        return Park;
        }
        public static ParkEF BLtoDL(Park park)
        {
            if (park == null)
            {
                return null;
            }

            ParkEF dataLayerPark = new ParkEF
            {
                Id = park.Id,
                Naam = park.Naam,
                Location = park.Locatie,
            };
            return dataLayerPark;
        }
    }
}

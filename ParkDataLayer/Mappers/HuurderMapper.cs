using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers
{
    public class HuurderMapper
    {
        public static Huurder DLtoBL(HuurderEF huurder)
        {
            if (huurder == null)
            {
                return null;
            }

            Contactgegevens contactgegevensBL = new Contactgegevens(huurder.Email, huurder.Telefoon, huurder.Adres);

            Huurder businessLogicHuurder = new Huurder(huurder.Id, huurder.Naam, contactgegevensBL);

            return businessLogicHuurder;
        }
        public static HuurderEF BLtoDL(Huurder huurder)
        {
            if (huurder == null)
            {
                return null;
            }

            HuurderEF dataLayerHuurder = new HuurderEF
            {
                Id = huurder.Id,
                Naam = huurder.Naam,
                Email = huurder.Contactgegevens.Email,
                Telefoon = huurder.Contactgegevens.Tel,
                Adres = huurder.Contactgegevens.Adres,
            };

            return dataLayerHuurder;
        }
    }
}

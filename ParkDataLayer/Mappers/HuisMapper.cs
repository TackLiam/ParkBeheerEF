using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers
{
    public class HuisMapper
    {
        public static Huis DLtoBL(HuisEF huisEF)
        {
            if (huisEF == null)
            {
                return null;
            }

            Huis huis = new Huis(huisEF.Id, huisEF.Straat, huisEF.Nr, huisEF.Actief, ParkMapper.DLtoBL(huisEF.Park));

            return huis;
        }
        public static HuisEF BLtoDL(Huis huis)
        {
            if (huis == null)
            {
                return null;
            }

            HuisEF huisEF = new HuisEF
            {
                Id = huis.Id,
                Straat = huis.Straat,
                Nr = huis.Nr,
                Actief = huis.Actief,
                Park = ParkMapper.BLtoDL(huis.Park),
            };

            return huisEF;
        }
    }
}

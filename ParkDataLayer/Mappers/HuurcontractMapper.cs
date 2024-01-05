using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers
{
    public class HuurcontractMapper
    {
        public static Huurcontract DLtoBL(HuurcontractEF contractEF)
        {
            if (contractEF == null)
            {
                return null;
            }

            Huurder huurderBL = HuurderMapper.DLtoBL(contractEF.Huurder);
            Huis huisBL = HuisMapper.DLtoBL(contractEF.Huis);

            Huurperiode huurperiode = new Huurperiode(contractEF.StartDatum, contractEF.AantalDagen);

            Huurcontract contract = new Huurcontract(contractEF.Id, huurperiode, huurderBL, huisBL);

            return contract;
        }
        public static HuurcontractEF BLtoDL(Huurcontract contract)
        {
            if (contract == null)
            {
                return null;
            }

            HuurcontractEF contractEF = new HuurcontractEF
            {
                Id = contract.Id,
                StartDatum = contract.Huurperiode.StartDatum,
                EindDatum = contract.Huurperiode.EindDatum,
                AantalDagen = contract.Huurperiode.Aantaldagen,
                Huurder = HuurderMapper.BLtoDL(contract.Huurder),
                Huis = HuisMapper.BLtoDL(contract.Huis),

            };

            return contractEF;
        }
    }
 
}

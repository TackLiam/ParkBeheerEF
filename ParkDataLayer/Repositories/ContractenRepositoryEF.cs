using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class ContractenRepositoryEF : IContractenRepository
    {
        private readonly ParkContext _context;
        public ContractenRepositoryEF(ParkContext context)
        {
            _context = context;
        }
        public void AnnuleerContract(Huurcontract contract)
        {
            HuurcontractEF contractEF = _context.Huurcontracten.FirstOrDefault(h => h.Id == contract.Id);
            if(contractEF != null)
            {
                _context.Huurcontracten.Remove(contractEF);
            }
            _context.SaveChanges();
        }

        public Huurcontract GeefContract(string id)
        {
            HuurcontractEF contractEF = _context.Huurcontracten.Find(id);
            return HuurcontractMapper.DLtoBL(contractEF);
        }

        public List<Huurcontract> GeefContracten(DateTime startDatum, DateTime? eindDatum)
        {
            List<HuurcontractEF> huurcontractenEF = new List<HuurcontractEF>();
            if(eindDatum == null)
            {
                huurcontractenEF = _context.Huurcontracten.Where(hc => hc.StartDatum >= startDatum).ToList();
            }
            else
            {
                huurcontractenEF = _context.Huurcontracten.Where(hc => hc.StartDatum >= startDatum && hc.EindDatum <= eindDatum).ToList();
            }
            List<Huurcontract> huurcontracten = huurcontractenEF
                .Select(HuurcontractMapper.DLtoBL)
                .ToList();
            return huurcontracten;
        }

        public bool HeeftContract(DateTime startDatum, int huurderId, int huisId)
        {
            return _context.Huurcontracten.Any(hc => hc.StartDatum == startDatum && hc.HuurderId == huurderId && hc.HuisId == huisId);
        }

        public bool HeeftContract(string id)
        {
            return _context.Huurcontracten.Any (hc => hc.Id == id);
        }

        public void UpdateContract(Huurcontract contract)
        {
            HuurcontractEF contractEF = _context.Huurcontracten.Find(contract.Id);
            ParkEF parkEF = _context.Parken.FirstOrDefault(p => p.Id == contract.Huis.Park.Id);
            if (parkEF != null)
            {

                HuisEF huisEF = _context.Huizen.FirstOrDefault(h => h.Id == contract.Huis.Id);
                if (huisEF != null)
                {
                    contractEF.Huis = huisEF;
                }
                contractEF.Huis.Park = parkEF;
            }

            HuurderEF huurderEF = _context.Huurders.FirstOrDefault(h => h.Id == contract.Huurder.Id);
            if (huurderEF != null)
            {
                contractEF.Huurder = huurderEF;
            }
            contractEF.StartDatum = contract.Huurperiode.StartDatum;
            contractEF.EindDatum = contract.Huurperiode.EindDatum;
            contractEF.AantalDagen = contract.Huurperiode.Aantaldagen;
            _context.SaveChanges();
        }

        public void VoegContractToe(Huurcontract contract)
        {
            HuurcontractEF contractEF = HuurcontractMapper.BLtoDL(contract);
            ParkEF parkEF = _context.Parken.FirstOrDefault(p => p.Id == contract.Huis.Park.Id);
            if (parkEF != null)
            {

                HuisEF huisEF = _context.Huizen.FirstOrDefault(h => h.Id == contract.Huis.Id);
                if (huisEF != null)
                {
                    contractEF.Huis = huisEF;
                }
                contractEF.Huis.Park = parkEF;
            }

            HuurderEF huurderEF = _context.Huurders.FirstOrDefault(h => h.Id == contract.Huurder.Id);
            if (huurderEF != null)
            {
                contractEF.Huurder = huurderEF;
            }

            _context.Huurcontracten.Add(contractEF);
            _context.SaveChanges();
        }
    }
}

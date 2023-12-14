using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using System;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class HuizenRepositoryEF : IHuizenRepository
    {
        private readonly ParkContext _context;
        public HuizenRepositoryEF(ParkContext context)
        {
            _context = context;
        }
        public Huis GeefHuis(int id)
        {
            HuisEF huis = _context.Huizen.Find(id);
            return HuisMapper.DLtoBL(huis);
        }

        public bool HeeftHuis(string straat, int nummer, Park park)
        {
            ParkEF parkEF = ParkMapper.BLtoDL(park);
            return _context.Huizen.Any(h => h.Straat == straat && h.Nr == nummer && h.Park == parkEF);
        }

        public bool HeeftHuis(int id)
        {
            return _context.Huizen.Any(h => h.Id == id);
        }

        public void UpdateHuis(Huis huis)
        {
            HuisEF  huisEF= _context.Huizen.Find(huis.Id);
            huisEF.Straat = huis.Straat;
            huisEF.Nr = huis.Nr;
            huisEF.Actief = huis.Actief;
            _context.SaveChanges();
        }

        public void VoegHuisToe(Huis huis)
        {
            HuisEF huisEF = HuisMapper.BLtoDL(huis);
            _context.Huizen.Add(huisEF);
            _context.SaveChanges();
        }
    }
}

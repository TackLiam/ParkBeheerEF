using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class HuurderRepositoryEF : IHuurderRepository
    {
        private readonly ParkContext _context;
        public HuurderRepositoryEF(ParkContext context)
        {
            _context = context;
        }
        public Huurder GeefHuurder(int id)
        {
            HuurderEF huurder = _context.Huurders.Find(id);
            return HuurderMapper.DLtoBL(huurder);
        }

        public List<Huurder> GeefHuurders(string naam)
        {
                return _context.Huurders
                    .Where(h => h.Naam.Contains(naam))
                    .Select(HuurderMapper.DLtoBL)
                    .ToList();
        }

        public bool HeeftHuurder(string naam, Contactgegevens contact)
        {
            return _context.Huurders.Any(h => h.Naam == naam && h.Telefoon == contact.Tel && h.Adres == contact.Adres && h.Email == contact.Email);
        }

        public bool HeeftHuurder(int id)
        {
            return _context.Huurders.Any(h => h.Id == id);
        }

        public void UpdateHuurder(Huurder huurder)
        {
            HuurderEF huurderEF = _context.Huurders.Find(huurder.Id);
            huurderEF.Naam = huurder.Naam;
            huurderEF.Email = huurder.Contactgegevens.Email;
            huurderEF.Adres = huurder.Contactgegevens.Adres;
            huurderEF.Telefoon = huurder.Contactgegevens.Tel;
            _context.SaveChanges();
        }

        public void VoegHuurderToe(Huurder huurder)
        {
            HuurderEF huurderEF = HuurderMapper.BLtoDL(huurder);
            _context.Huurders.Add(huurderEF);
            _context.SaveChanges();
        }
    }
}

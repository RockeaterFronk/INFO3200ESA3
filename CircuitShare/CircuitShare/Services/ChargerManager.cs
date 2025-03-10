﻿using CircuitShare.Entities;

namespace CircuitShare.Services
{
    public class ChargerManager
    {
        private CircuitShareDbContext _circuitShareDbContext;

        /// <summary>
        /// The constructor for ChargerManager
        /// </summary>
        /// <param name="circuitShareDbContext"> the circuit share database context</param>
        public ChargerManager(CircuitShareDbContext circuitShareDbContext)
        {
            _circuitShareDbContext = circuitShareDbContext;
        }

        /// <summary>
        /// gets all of the chargers from the db context
        /// </summary>
        /// <returns>a list of charger objects, ordered by hourly rate from the db context</returns>
        public List<Charger> GetChargers()
        {
            //changed to order by hourly rate
            return _circuitShareDbContext.Chargers.OrderBy(i => i.HourlyRate).ToList();
        }

        /// <summary>
        /// Returns the charger at the targeted id, if it exists
        /// </summary>
        /// <param name="id">Id of the charger object</param>
        /// <returns></returns>
        public Charger? GetChargerById(int id)
        {
            return GetBaseQuery()
                    .Where(g => g.ChargerId == id)
                    .FirstOrDefault();
        }

        /// <summary>
        /// Adds a new charger object to the Chargers dbset
        /// </summary>
        /// <param name="charger">The charger being passed in</param>
        /// <returns>the id of the newly created charger</returns>
        public int AddNewCharger(Charger charger)
        {
            _circuitShareDbContext.Chargers.Add(charger);
            _circuitShareDbContext.SaveChanges();
            return charger.ChargerId;
        }

        private IQueryable<Charger> GetBaseQuery()
        {
            return _circuitShareDbContext.Chargers;
        }
    }
}

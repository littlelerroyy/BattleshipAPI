namespace BattleshipAPI.Models
{
    public abstract class Ship
    {

        public List<Location> Locations { get; set; }
        public bool Destroyed = false;

        public string AnnounceHit()
        {
            if (this.Destroyed) 
            {
                return "You Hit and Destroyed A " + this.GetType().Name;
            }            

            return "You Hit A " + this.GetType().Name;
        }

        public bool AddHitMarkerToShipLocation(uint PosX, uint PosY)
        {
            var Location = this.Locations.Where(Location => Location.xAxis == PosX && Location.xAxis == PosY).First();
            Location.BeenHit = true;

            //Calculate if the ship has all of its locations hit
            if (this.IsShipDestroyed()) 
            { 
            this.Destroyed = true;
            }

            return true;
        }

        private bool IsShipDestroyed()
        {
            return this.Locations.TrueForAll(Location => Location.BeenHit == true);
        }
    }
}

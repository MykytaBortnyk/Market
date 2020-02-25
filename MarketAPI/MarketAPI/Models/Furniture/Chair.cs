using System;
namespace MarketAPI.Models.Furniture
{
    public class Chair : Product
    {
        public int MaxWeight { get; set; }

        public string BodyMaterial { get; set; }

        public string UpholsteryMaterial { get; set; }

        public string Body { get; set; }

        public string Upholstery { get; set; }

        public int SeatSize { get; set; }

        public string Basetype { get; set; }
    }
}

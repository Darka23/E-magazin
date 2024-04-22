using E_magazin.Data.Models;

namespace E_magazin.ViewModels
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal BuyingPrice { get; set; }

        public string ManufacturerName { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public string UnitOfMeasureName { get; set; }

        public UnitOfMeasure UnitOfMeasure { get; set; }

        public string CategoryName { get; set; }

        public double Amount { get; set; }

        public bool IsSoldOut { get; set; }
    }
}

using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item in a sale.
    /// </summary>
    public class SaleItem : BaseEntity, ISaleItem
    {

        /// <summary>
        /// Gets or sets the quantity of the product in the sale.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price per unit of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets the total value of the item (Quantity * UnitPrice).
        /// </summary>
        public decimal TotalPrice { get; private set; } = 0;

        /// <summary>
        /// Gets or sets the discount applied to this item.
        /// </summary>
        public decimal Discount { get; set; } = 0;

        /// <summary>
        /// Gets or sets the product unique identifier for the sale item.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product unique identifier for the sale item.
        /// </summary>
        public Guid SaleId { get; set; }

        public SaleItem()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public SaleItem(int quantity, decimal unitPrice, Guid productId, Guid saleId)
        {
            ValidationMoreThanTwelve( productId, quantity);
            Quantity = quantity;
            UnitPrice = unitPrice;
            SetDiscount(quantity);
            CalculeTotalPrice();
            ProductId = productId;
            SaleId = saleId;
            CreatedAt = DateTime.UtcNow;
        }
        /// <summary>
        /// Gets the unique identifier of the item sale.
        /// </summary>
        /// <returns>The item sale's ID as a string.</returns>
        string ISaleItem.Id => Id.ToString();

        Guid? ISaleItem.SaleId => SaleId;

        public void SetQuantity(int quantity)
        {
            if (Quantity == quantity) return;
            Quantity = quantity;
            SetDiscount(quantity);
        }
        public void SetDiscount(int quantity)
        {
            decimal discount = 0;
            if (quantity >= 4 && quantity < 10)
                discount = 0.10m;
            else if (quantity >= 10)
                discount = 0.20m;
            Discount = discount;
            TotalPrice = (Quantity * UnitPrice) * (1 - Discount);
            UpdatedAt = DateTime.UtcNow;
            CalculeTotalPrice();
        }
        public void SetUnitPrice(decimal unitPrice)
        {
            if(UnitPrice == unitPrice) return;
            UnitPrice = unitPrice;
            TotalPrice = (Quantity * UnitPrice) * (1 - Discount);
            UpdatedAt = DateTime.UtcNow;
            CalculeTotalPrice();

        }
        public void CalculeTotalPrice()
        {
            TotalPrice = (Quantity * UnitPrice) * (1 - Discount);
        }

        public void Update(Guid productId, int quantity , decimal unitPrice)
        {
            ValidationMoreThanTwelve(productId, quantity);
            SetQuantity(quantity); 
            SetUnitPrice(unitPrice);
        }
        public static void ValidationMoreThanTwelve(Guid productId, int quantity)
        {
            if (quantity > 20)
                throw new InvalidOperationException($"Product {productId} exceeds the 20 items limit.");
        }
    }
}

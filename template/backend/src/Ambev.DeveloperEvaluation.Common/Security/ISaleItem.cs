namespace Ambev.DeveloperEvaluation.Common.Security
{
    public interface ISaleItem
    {
        /// <summary>
        /// Gets the unique identifier for the item.
        /// </summary>
        /// <returns>The sale ID as a string.</returns>
        public string Id { get; }

        /// <summary>
        /// Gets or sets the product unique identifier for the sale item.
        /// </summary>
        /// <returns>The product ID as a string.</returns>
        public string ProductId { get; }

        /// <summary>
        /// Gets or sets the quantity of the product in the sale.
        /// </summary>
        /// <returns>The total quantity of the product.</returns>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price per unit of the product.
        /// </summary>
        /// <returns>The unit price of the product</returns>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets the total value of the item (Quantity * UnitPrice).
        /// </summary>
        /// <returns>The total price of the products</returns>
        public decimal TotalPrice { get; }

        /// <summary>
        /// Gets the date and time when the item sale was created.
        /// </summary>
        /// <returns>The creation date of the item sale.</returns>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Gets the date and time of the last update to the item sale.
        /// </summary>
        /// <returns>The last update date.</returns>
        public DateTime? UpdatedAt { get; }

        /// <summary>
        /// Gets the date and time of the last update to the item sale.
        /// </summary>
        /// <returns>The last update date.</returns>
        public Guid? SaleId { get; }

        void SetQuantity(int quantity);
        void SetDiscount(int quantity);
        void SetUnitPrice(decimal unitPrice);

    }
}

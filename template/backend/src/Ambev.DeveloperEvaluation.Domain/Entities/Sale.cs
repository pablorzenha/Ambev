using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction in the system.
    /// Includes customer information, date of sale, total amount, and the list of items.
    /// </summary>
    public class Sale : BaseEntity, ISale
    {

        /// <summary>
        /// Gets the sale number.
        /// </summary>
        /// <returns>The sale ID as a string.</returns>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// This value is calculated based on the sum of item prices.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the Id of the customer associated with the sale.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the Id of the branch associated with the sale.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the sale was made.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets the Sale item's status in the system.
        /// </summary>
        public SaleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the list of items associated with the sale.
        /// </summary>
        public List<SaleItem> Items { get; set; } = new();

        public Sale( string saleNumber, Guid customerId, Guid branchId, DateTime date, SaleStatus status)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            CustomerId = customerId;
            BranchId = branchId;
            Date = date;
            Status = status;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        /// <returns>The sale's ID as a string.</returns>
        string ISale.Id => Id.ToString();

        /// <summary>
        /// Gets the unique identifier of the customer.
        /// </summary>
        /// <returns>The customer's ID as a string.</returns>
        string ISale.CustomerId => CustomerId.ToString();

        /// <summary>
        /// Gets the unique identifier of the branch.
        /// </summary>
        /// <returns>The branch's ID as a string.</returns>
        string ISale.BranchId => BranchId.ToString();

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItem"/> class.
        /// </summary>
        string ISale.Status => Status.ToString();

        List<ISaleItem> ISale.Items => Items.Cast<ISaleItem>().ToList();

        public void AddItem(Guid productId, int quantity, decimal unitPrice)
        {
            this.Items.Add(new SaleItem(quantity, unitPrice, productId, Id));
        }

        public void Update(string saleNumber, DateTime date, Guid customerId, Guid branchId)
        {
            SetSaleNumber(saleNumber);
            SetDate(date);
            SetCustomer(customerId);
            SetBranch(branchId);
        }

        public void SetSaleNumber(string saleNumber)
        {
            if (SaleNumber == saleNumber) return;
            SaleNumber = saleNumber;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDate(DateTime date)
        {
            if (Date == date) return;
            Date = date;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetCustomer(Guid customerId)
        {
            if (CustomerId == customerId) return;
            CustomerId = customerId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetBranch(Guid branchId)
        {
            if (BranchId == branchId) return;
            BranchId = branchId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveItem(Guid productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                Items.Remove(item);
        }

        public void CalculateTotal()
        {
            TotalAmount = Items.Sum(i => i.TotalPrice);
        }
        public void ReplaceItems()
        {
            var consolidateItems = ConsolidateItems();
            Items.Clear();
            Items.AddRange(consolidateItems);
            CalculateTotal();
        }

        public List<SaleItem> ConsolidateItems()
        {
            var grouped = new Dictionary<Guid, SaleItem>();
            foreach (var item in this.Items)
            {
                if (!grouped.ContainsKey(item.ProductId))
                    grouped[item.ProductId] = new SaleItem(item.Quantity, item.UnitPrice, item.ProductId, Id);
                else
                {
                    var existing = grouped[item.ProductId];

                    if (existing.UnitPrice != item.UnitPrice)
                        throw new InvalidOperationException("Multiple unit prices found for the same product.");

                    existing.Quantity += item.Quantity;
                }
            }

            foreach (var entry in grouped.Values)
            {
                if (entry.Quantity > 20)
                    throw new InvalidOperationException("Cannot sell more than 20 units of the same product.");

                entry.SetDiscount(entry.Quantity);
            }
            return grouped.Values.ToList();
        }
    }
}


using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class ListSaleResult
    {
        public int TotalSize { get; set; }
        public List<Sale> Items { get; set; }
        public ListSaleResult(int totalSize, List<Sale> items) 
        { 
            this.TotalSize = totalSize;
            this.Items = items;
        }
    }
}

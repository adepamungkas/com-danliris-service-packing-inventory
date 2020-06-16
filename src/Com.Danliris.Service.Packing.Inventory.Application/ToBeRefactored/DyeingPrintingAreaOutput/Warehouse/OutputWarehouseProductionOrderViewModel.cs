﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse
{
    public class OutputWarehouseProductionOrderViewModel: BaseViewModel
    {
        public ProductionOrder ProductionOrder { get; set; }
        public string CartNo { get; set; }
        public string PackingInstruction { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public int BuyerId { get; set; }
        public string Buyer { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UomUnit { get; set; }
        public string Remark { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public double Balance { get; set; }
        public double PreviousBalance { get; set; }

        public int InputId { get; set; }
        public string ProductionOrderNo { get; set; }
        public bool HasNextAreaDocument { get; set; }
        //public bool IsChecked { get; set; }
        public string Material { get; set; }
        public decimal MtrLength { get; set; }
        public decimal YdsLength { get; set; }
        public decimal Quantity { get; set; }
        public string PackagingType { get; set; }
        public string PackagingUnit { get; set; }
        public decimal PackagingQty { get; set; }
        public double QtyOrder { get; set; }
        public long DeliveryOrderSalesId { get; set; }
        public string DeliveryOrderSalesNo { get; set; }
    }
}

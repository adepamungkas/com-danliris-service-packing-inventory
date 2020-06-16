﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public class InputAvalItemViewModel : BaseViewModel
    {
        public string AvalType { get; set; }
        public string AvalCartNo { get; set; }
        public string AvalUomUnit { get; set; }
        public double AvalQuantity { get; set; }
        public double AvalQuantityKg { get; set; }
        public bool HasOutputDocument { get; set; }
        public bool IsChecked { get; set; }

        public int ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }
        public string CartNo { get; set; }
        public int BuyerId { get; set; }
        public string Buyer { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string Remark { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public double Balance { get; set; }
        public string PackingInstruction { get; set; }
        public string ProductionOrderType { get; set; }
        public double ProductionOrderOrderQuantity { get; set; }
        public string PackagingType { get; set; }
        public decimal PackagingQty { get; set; }
        public string PackagingUnit { get; set; }
        public int DyeingPrintingAreaOutputProductionOrderId { get; set; }
        public int DyeingPrintingAreaInputProductionOrderId { get; set; }
        public ProductionOrder ProductionOrder { get; set; }
        public string Area { get; set; }
        public string UomUnit { get; set; }
        public double QtyOrder { get; set; }
    }
}

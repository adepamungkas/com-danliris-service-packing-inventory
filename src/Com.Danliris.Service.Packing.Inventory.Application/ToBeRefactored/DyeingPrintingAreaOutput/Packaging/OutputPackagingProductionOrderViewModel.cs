﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging
{
    public class OutputPackagingProductionOrderViewModel : BaseViewModel
    {
        public ProductionOrder ProductionOrder { get; set; }
        public string ProductionOrderNo { get; set; }
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
        public string Material { get; set; }
        public string PackagingUnit { get; set; }
        public decimal PackagingQTY { get; set; }
        public string PackagingType { get; set; }
        public double QtyOrder { get; set; }
        public string Keterangan { get; set; }
        public double QtyOut { get; set; }
    }
}

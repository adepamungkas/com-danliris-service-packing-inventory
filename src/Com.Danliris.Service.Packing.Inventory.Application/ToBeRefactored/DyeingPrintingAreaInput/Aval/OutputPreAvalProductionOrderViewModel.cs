﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public class OutputPreAvalProductionOrderViewModel : BaseViewModel
    {
        public ProductionOrder ProductionOrder { get; set; }
        public string CartNo { get; set; }
        public int BuyerId { get; set; }
        public string Buyer { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UomUnit { get; set; }
        public string Remark { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public double Balance { get; set; }
        public string PackingInstruction { get; set; }
        public double AvalConnectionLength { get; set; }
        public double AvalALength { get; set; }
        public double AvalBLength { get; set; }
        public double QtyOrder { get; set; }
        public string AvalType { get; set; }
        public int DyeingPrintingAreaInputProductionOrderId { get; set; }
        //public string AvalType { get; set; }
        //public string AvalCartNo { get; set; }
        //public string AvalUomUnit { get; set; }
        //public double AvalQuantity { get; set; }
        //public double AvalQuantityKg { get; set; }
    }
}

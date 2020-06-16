﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.ShipmentInput
{
    public class ShipmentInputViewModel : BaseViewModel
    {
        public string Area { get; set; }
        public string Shift { get; set; }
        public int PreShipmentAreaId { get; set; }
        public string BonNo { get; set; }
        public DeliveryOrderSales DeliveryOrderSales { get; set; }
        public string ProductionOrderNo { get; set; }
        public string BuyerName { get; set; }
        public string Construction { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string Grade { get; set; }
        public double PackingQTY { get; set; }
        public string PackingUOM { get; set; }
        public decimal PackingBalance { get; set; }
        public string UomUnit { get; set; }
    }
}

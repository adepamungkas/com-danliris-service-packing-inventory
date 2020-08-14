﻿using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Data;
using OfficeOpenXml;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Globalization;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse
{
    public class StockWarehouseService : IStockWarehouseService
    {
        private readonly IDyeingPrintingAreaOutputRepository _outputBonRepository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputSppRepository;
        private readonly IDyeingPrintingAreaInputRepository _inputBonRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputSppRepository;

        private const string OUT = "OUT";
        private const string IN = "IN";
        private const string AWAL = "AWAL";
        private const string TRANSFORM = "TRANSFORM";
        private const string ADJ_IN = "ADJ IN";
        private const string ADJ_OUT = "ADJ OUT";

        private const string IM = "IM";
        private const string TR = "TR";
        private const string PC = "PC";
        private const string GJ = "GJ";
        private const string GA = "GA";
        private const string SP = "SP";

        private const string INSPECTIONMATERIAL = "INSPECTION MATERIAL";
        private const string TRANSIT = "TRANSIT";
        private const string PACKING = "PACKING";
        private const string GUDANGJADI = "GUDANG JADI";
        private const string GUDANGAVAL = "GUDANG AVAL";
        private const string SHIPPING = "SHIPPING";

        public StockWarehouseService(IServiceProvider serviceProvider)
        {
            _outputBonRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputSppRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _inputBonRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _outputSppRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private IEnumerable<SimpleReportViewModel> GetAwalData(DateTimeOffset dateFrom, string area, IEnumerable<long> productionOrderIds, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId)
        {
            var queryTransform = _movementRepository.ReadAll()
                .Where(s => s.Area == area && s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date < dateFrom.Date && productionOrderIds.Contains(s.ProductionOrderId));

            if (!string.IsNullOrEmpty(unit))
            {
                queryTransform = queryTransform.Where(s => s.Unit == unit);
            }
            if (!string.IsNullOrEmpty(packingType))
            {
                queryTransform = queryTransform.Where(s => s.PackingType == packingType);
            }
            if (!string.IsNullOrEmpty(construction))
            {
                queryTransform = queryTransform.Where(s => s.Construction == construction);
            }
            if (!string.IsNullOrEmpty(buyer))
            {
                queryTransform = queryTransform.Where(s => s.Buyer == buyer);
            }
            if (productionOrderId != 0)
            {
                queryTransform = queryTransform.Where(s => s.ProductionOrderId == productionOrderId);
            }

            var data = queryTransform.Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.ProductionOrderId, s.ProductionOrderNo, s.ProductionOrderType, s.Construction, s.Color,
                   s.Grade, s.Remark, s.Motif, s.Unit, s.UomUnit, s.Balance, s.PackingType)).ToList();

            var result = data.GroupBy(s => new { s.ProductionOrderId, s.Grade, s.Remark, s.PackingType }).Select(d => new SimpleReportViewModel()
            {
                ProductionOrderId = d.Key.ProductionOrderId,
                Type = AWAL,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Grade = d.Key.Grade,
                Jenis = d.Key.PackingType,
                Ket = d.Key.Remark,
                Motif = d.First().Motif,
                NoSpp = d.First().ProductionOrderNo,
                Satuan = d.First().UomUnit,
                Unit = d.First().Unit,
                Quantity = d.Where(e => e.Type == IN).Sum(e => e.Balance) - d.Where(e => e.Type == OUT).Sum(e => e.Balance)
                    + d.Where(e => e.Type == ADJ_IN || e.Type == ADJ_OUT).Sum(e => e.Balance)

            });

            return result;
        }

        private IEnumerable<SimpleReportViewModel> GetDataByDate(DateTime startDate, DateTimeOffset dateReport, string area, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId)
        {
            var queryTransform = _movementRepository.ReadAll()
                   .Where(s => s.Area == area &&
                        startDate.Date <= s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date &&
                        s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date <= dateReport.Date);

            if (!string.IsNullOrEmpty(unit))
            {
                queryTransform = queryTransform.Where(s => s.Unit == unit);
            }
            if (!string.IsNullOrEmpty(packingType))
            {
                queryTransform = queryTransform.Where(s => s.PackingType == packingType);
            }
            if (!string.IsNullOrEmpty(construction))
            {
                queryTransform = queryTransform.Where(s => s.Construction == construction);
            }
            if (!string.IsNullOrEmpty(buyer))
            {
                queryTransform = queryTransform.Where(s => s.Buyer == buyer);
            }
            if (productionOrderId != 0)
            {
                queryTransform = queryTransform.Where(s => s.ProductionOrderId == productionOrderId);
            }

            var data = queryTransform.Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.ProductionOrderId, s.ProductionOrderNo, s.ProductionOrderType, s.Construction, s.Color,
                        s.Grade, s.Remark, s.Motif, s.Unit, s.UomUnit, s.Balance, s.PackingType)).ToList();

            var result = data.GroupBy(s => new { s.ProductionOrderId, s.Type, s.Grade, s.Remark, s.PackingType }).Select(d => new SimpleReportViewModel()
            {
                ProductionOrderId = d.Key.ProductionOrderId,
                Type = d.Key.Type,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Grade = d.Key.Grade,
                Jenis = d.Key.PackingType,
                Ket = d.Key.Remark,
                Motif = d.First().Motif,
                NoSpp = d.First().ProductionOrderNo,
                Satuan = d.First().UomUnit,
                Unit = d.First().Unit,
                Quantity = d.Sum(e => e.Balance)
            });

            return result;
        }

        public List<ReportStockWarehouseViewModel> GetReportData(DateTimeOffset dateReport, string zona, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId)
        {
            var startDate = new DateTime(dateReport.Year, dateReport.Month, 1);
            var dataSearchDate = GetDataByDate(startDate, dateReport, zona, offset, unit, packingType, construction, buyer, productionOrderId);
            var productionOrderIds = dataSearchDate.Select(e => e.ProductionOrderId);
            var dataAwal = GetAwalData(startDate, zona, productionOrderIds, offset, unit, packingType, construction, buyer, productionOrderId);
            var joinData2 = dataSearchDate.Concat(dataAwal);

            var result = joinData2.GroupBy(d => new { d.ProductionOrderId, d.Grade, d.Jenis, d.Ket }).Select(e => new ReportStockWarehouseViewModel()
            {
                ProductionOrderId = e.Key.ProductionOrderId,
                NoSpp = e.First().NoSpp,
                Color = e.First().Color,
                Construction = e.First().Construction,
                Grade = e.Key.Grade,
                Jenis = e.Key.Jenis,
                Ket = e.Key.Ket,
                Motif = e.First().Motif,
                Satuan = e.First().Satuan,
                Unit = e.First().Unit,
                Awal = e.FirstOrDefault(d => d.Type == AWAL) != null ? e.FirstOrDefault(d => d.Type == AWAL).Quantity : 0,
                Masuk = e.FirstOrDefault(d => d.Type == IN) != null ? e.FirstOrDefault(d => d.Type == IN).Quantity : 0,
                Keluar = (e.FirstOrDefault(d => d.Type == OUT) != null ? e.FirstOrDefault(d => d.Type == OUT).Quantity : 0)
                    - (e.FirstOrDefault(d => d.Type == ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == ADJ_IN).Quantity : 0)
                    - (e.FirstOrDefault(d => d.Type == ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == ADJ_OUT).Quantity : 0),
                Akhir = (e.FirstOrDefault(d => d.Type == AWAL) != null ? e.FirstOrDefault(d => d.Type == AWAL).Quantity : 0)
                    + (e.FirstOrDefault(d => d.Type == IN) != null ? e.FirstOrDefault(d => d.Type == IN).Quantity : 0)
                    - (e.FirstOrDefault(d => d.Type == OUT) != null ? e.FirstOrDefault(d => d.Type == OUT).Quantity : 0)
                    + (e.FirstOrDefault(d => d.Type == ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == ADJ_IN).Quantity : 0)
                    + (e.FirstOrDefault(d => d.Type == ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == ADJ_OUT).Quantity : 0)
            });

            return result.Where(s => s.Awal != 0 || s.Masuk != 0 || s.Keluar != 0 || s.Akhir != 0).ToList();
        }

        public MemoryStream GenerateExcel(DateTimeOffset dateReport, string zona, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId)
        {
            var data = GetReportData(dateReport, zona, offset, unit, packingType, construction, buyer, productionOrderId);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Ket", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Awal", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Masuk", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keluar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Akhir", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });

            if (data.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", 0, 0, 0, 0, "");
            }
            else
            {
                foreach (var item in data)
                {
                    dt.Rows.Add(item.NoSpp, item.Construction, item.Unit, item.Motif, item.Color, item.Grade, item.Jenis,
                        item.Ket, item.Awal.ToString("N2", CultureInfo.InvariantCulture), item.Masuk.ToString("N2", CultureInfo.InvariantCulture), item.Keluar.ToString("N2", CultureInfo.InvariantCulture),
                        item.Akhir.ToString("N2", CultureInfo.InvariantCulture), item.Satuan);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, string.Format("Laporan Stock {0}", zona)) }, true);
        }

        private IEnumerable<PackingDataViewModel> GetAwalPackingData(DateTimeOffset dateFrom, string area, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string grade)
        {
            var queryTransform = _movementRepository.ReadAll()
                   .Where(s => s.Area == area &&
                        s.Unit == unit &&
                        s.PackingType == packingType &&
                        s.Construction == construction &&
                        s.Grade == grade &&
                        s.ProductionOrderId == productionOrderId &&
                        s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date < dateFrom.Date);


            if (!string.IsNullOrEmpty(buyer))
            {
                queryTransform = queryTransform.Where(s => s.Buyer == buyer);
            }

            var data = queryTransform.Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.ProductionOrderId, s.PackagingQty, s.PackagingUnit, s.PackagingLength, s.Balance)).ToList();

            var result = data.GroupBy(s => new { s.PackagingUnit, s.PackagingLength }).Select(d => new PackingDataViewModel()
            {
                Type = AWAL,
                PackagingLength = d.Key.PackagingLength,
                PackagingUnit = d.Key.PackagingUnit,
                PackagingQty = d.Where(e => e.Type == IN).Sum(e => e.PackagingQty) - d.Where(e => e.Type == OUT).Sum(e => e.PackagingQty)
                    + d.Where(e => e.Type == ADJ_IN || e.Type == ADJ_OUT).Sum(e => e.PackagingQty),
                Balance = d.Where(e => e.Type == IN).Sum(e => e.Balance) - d.Where(e => e.Type == OUT).Sum(e => e.Balance)
                    + d.Where(e => e.Type == ADJ_IN || e.Type == ADJ_OUT).Sum(e => e.Balance)

            });

            return result;
        }

        private IEnumerable<PackingDataViewModel> GetPackingDataByDate(DateTime startDate, DateTimeOffset dateReport, string area, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string grade)
        {
            var queryTransform = _movementRepository.ReadAll()
                   .Where(s => s.Area == area &&
                        s.Unit == unit &&
                        s.PackingType == packingType &&
                        s.Construction == construction &&
                        s.Grade == grade &&
                        s.ProductionOrderId == productionOrderId &&
                        startDate.Date <= s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date &&
                        s.Date.ToOffset(new TimeSpan(offset, 0, 0)).Date <= dateReport.Date);

            if (!string.IsNullOrEmpty(buyer))
            {
                queryTransform = queryTransform.Where(s => s.Buyer == buyer);
            }

            var data = queryTransform.Select(s => new DyeingPrintingAreaMovementModel(s.Date, s.Area, s.Type, s.ProductionOrderId, s.PackagingQty, s.PackagingUnit, s.PackagingLength, s.Balance)).ToList();

            var result = data.GroupBy(s => new { s.Type, s.PackagingUnit, s.PackagingLength }).Select(d => new PackingDataViewModel()
            {
                Type = d.Key.Type,
                PackagingLength = d.Key.PackagingLength,
                PackagingUnit = d.Key.PackagingUnit,
                PackagingQty = d.Sum(e => e.PackagingQty),
                Balance = d.Sum(e => e.Balance)
            });

            return result;
        }

        public List<PackingDataViewModel> GetPackingData(DateTimeOffset dateReport, string zona, int offset, string unit, string packingType, string construction, string buyer, long productionOrderId, string grade)
        {
            var startDate = new DateTime(dateReport.Year, dateReport.Month, 1);
            var dataSearchDate = GetPackingDataByDate(startDate, dateReport, zona, offset, unit, packingType, construction, buyer, productionOrderId, grade);
            var dataAwal = dataSearchDate.Count() > 0 ? GetAwalPackingData(startDate, zona, offset, unit, packingType, construction, buyer, productionOrderId, grade) : new List<PackingDataViewModel>();
            var joinData2 = dataSearchDate.Concat(dataAwal);

            var result = joinData2.GroupBy(d => new { d.PackagingUnit, d.PackagingLength }).Select(e => new PackingDataViewModel()
            {
                PackagingLength = e.Key.PackagingLength,
                PackagingUnit = e.Key.PackagingUnit,
                Balance = (e.FirstOrDefault(d => d.Type == AWAL) != null ? e.FirstOrDefault(d => d.Type == AWAL).Balance : 0)
                    + (e.FirstOrDefault(d => d.Type == IN) != null ? e.FirstOrDefault(d => d.Type == IN).Balance : 0)
                    - (e.FirstOrDefault(d => d.Type == OUT) != null ? e.FirstOrDefault(d => d.Type == OUT).Balance : 0)
                    + (e.FirstOrDefault(d => d.Type == ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == ADJ_IN).Balance : 0)
                    + (e.FirstOrDefault(d => d.Type == ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == ADJ_OUT).Balance : 0),
                PackagingQty = (e.FirstOrDefault(d => d.Type == AWAL) != null ? e.FirstOrDefault(d => d.Type == AWAL).PackagingQty : 0)
                    + (e.FirstOrDefault(d => d.Type == IN) != null ? e.FirstOrDefault(d => d.Type == IN).PackagingQty : 0)
                    - (e.FirstOrDefault(d => d.Type == OUT) != null ? e.FirstOrDefault(d => d.Type == OUT).PackagingQty : 0)
                    + (e.FirstOrDefault(d => d.Type == ADJ_IN) != null ? e.FirstOrDefault(d => d.Type == ADJ_IN).PackagingQty : 0)
                    + (e.FirstOrDefault(d => d.Type == ADJ_OUT) != null ? e.FirstOrDefault(d => d.Type == ADJ_OUT).PackagingQty : 0)
            });

            return result.Where(s => s.Balance != 0).ToList();
        }
    }
}

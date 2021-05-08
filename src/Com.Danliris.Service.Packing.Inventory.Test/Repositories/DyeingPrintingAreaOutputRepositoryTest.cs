﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class DyeingPrintingAreaOutputRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, DyeingPrintingAreaOutputRepository, DyeingPrintingAreaOutputModel, DyeingPrintingAreaOutputDataUtil>
    {
        private const string ENTITY = "DyeingPrintingAreaInput";
        public DyeingPrintingAreaOutputRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetDbSet();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_ReadAllIgnoreQueryFilter()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.ReadAllIgnoreQueryFilter();

            Assert.NotEmpty(result);
        }

        [Fact]
        public virtual async Task Should_Success_Update_2()
        {
            string testName = GetCurrentMethod() + "Update2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetModel();

            int index = 0;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var spp = data.DyeingPrintingAreaOutputProductionOrders.ElementAtOrDefault(index++);
                item.DyeingPrintingAreaOutputId = data.Id;
                item.Id = spp.Id;
            }

            var result = await repo2.UpdateAsync(data.Id, model);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateFromInput()
        {
            string testName = GetCurrentMethod() + "UpdateFromInput";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            var result = await repo.UpdateFromInputAsync(data.Id, true);
            Assert.NotEqual(0, result);
        }
        [Fact]
        public virtual async Task Should_Success_UpdateFromInput2()
        {
            string testName = GetCurrentMethod() + "UpdateFromInput";
            var dbContext = DbContext(testName);

            //var serviceProviderMock = GetServiceProviderMock(dbContext).Object;
            var outSppMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            outSppMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);
            var serviceProviderMock = GetServiceProviderMock(dbContext);
            serviceProviderMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outSppMock.Object);
            var serviceProvider = serviceProviderMock.Object;
            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider);


            var data = await DataUtil(repo, dbContext).GetTestData();
            var test = new List<int> { data.Id };
            var result = await repo.UpdateFromInputAsync(data.Id, true, test);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateFromInput2_WithSameNumberSPP()
        {
            string testName = GetCurrentMethod() + "UpdateFromInput";
            var dbContext = DbContext(testName);

            //var serviceProviderMock = GetServiceProviderMock(dbContext).Object;
            var outSppMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            outSppMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);
            var serviceProviderMock = GetServiceProviderMock(dbContext);
            serviceProviderMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outSppMock.Object);
            var serviceProvider = serviceProviderMock.Object;
            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider);


            var data = await DataUtil(repo, dbContext).GetTestData();

            List<int> test = new List<int>();
            foreach (var item in data.DyeingPrintingAreaOutputProductionOrders)
            {
                test.Add(item.Id);
            }
            var result = await repo.UpdateFromInputAsync(data.Id, true, test);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateFromInputNextAreaFlagParentOnly()
        {
            string testName = GetCurrentMethod() + "UpdateFromInputNextAreaFlagParentOnly";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            var result = await repo.UpdateFromInputNextAreaFlagParentOnlyAsync(data.Id, true);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_Update_3()
        {
            string testName = GetCurrentMethod() + "Update_3";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyWithDOModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetWithDOModel();

            int index = 0;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var spp = data.DyeingPrintingAreaOutputProductionOrders.ElementAtOrDefault(index++);
                item.DyeingPrintingAreaOutputId = data.Id;
                item.Id = spp.Id;
            }

            var result = await repo2.UpdateAsync(data.Id, model);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateHasSalesInvoice()
        {
            string testName = GetCurrentMethod() + "UpdateHasSalesInvoice";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            var result = await repo.UpdateHasSalesInvoice(data.Id, true);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteIMArea()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeleteIMArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteIMArea_Produksi()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            data.SetDestinationArea("PRODUKSI", "", "");
            var result = await repo.DeleteIMArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMArea()
        {
            string testName = GetCurrentMethod() + "UpdateIMArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();

            var result = await repo2.UpdateIMArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMArea_Produksi()
        {
            string testName = GetCurrentMethod() + "UpdateIMAreaProduksi";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();
            model.SetDestinationArea("PRODUKSI", "", "");
            var result = await repo2.UpdateIMArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMAdj()
        {
            string testName = GetCurrentMethod() + "UpdateIMAdj";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();

            var result = await repo2.UpdateAdjustmentData(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMAdj_Shipping()
        {
            string testName = GetCurrentMethod() + "UpdateIMAdj_Shipping";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            emptyData.SetArea("SHIPPING", "", "");
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();
            model.SetArea("SHIPPING", "", "");
            var result = await repo2.UpdateAdjustmentData(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMAdj_2()
        {
            string testName = GetCurrentMethod() + "UpdateIMAdj_2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();

            data.DyeingPrintingAreaOutputProductionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();


            var result = await repo2.UpdateAdjustmentData(data.Id, data, emptyData);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMAdj_2_SH()
        {
            string testName = GetCurrentMethod() + "UpdateIMAdj_22_SH";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModel();
            emptyData.SetArea("SHIPPING", "", "");
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();

            data.DyeingPrintingAreaOutputProductionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();


            var result = await repo2.UpdateAdjustmentData(data.Id, data, emptyData);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMAdj_3()
        {
            string testName = GetCurrentMethod() + "UpdateIMAdj_3";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();

            var item = new DyeingPrintingAreaOutputProductionOrderModel();
            item.Id = 0;
            data.DyeingPrintingAreaOutputProductionOrders.Add(item);


            var result = await repo2.UpdateAdjustmentData(data.Id, data, emptyData);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMAdj_3_SH()
        {
            string testName = GetCurrentMethod() + "UpdateIMAdj_3_SH";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                 .ReturnsAsync(1);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModel();
            emptyData.SetArea("SHIPPING", "", "");
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();

            var item = new DyeingPrintingAreaOutputProductionOrderModel();
            item.Id = 0;
            data.DyeingPrintingAreaOutputProductionOrders.Add(item);


            var result = await repo2.UpdateAdjustmentData(data.Id, data, emptyData);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMArea2()
        {
            string testName = GetCurrentMethod() + "UpdateIMArea2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModelForUpdateBefore();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelForUpdateAfter();

            var result = await repo2.UpdateIMArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMArea3()
        {
            string testName = GetCurrentMethod() + "UpdateIMArea3";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModelBefore();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelForUpdateAfter2();
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.Id = dbModel.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().Id;
            }
            var result = await repo2.UpdateIMArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMArea4()
        {
            string testName = GetCurrentMethod() + "UpdateIMArea4";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModelBefore();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelForUpdateAfter2();
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.Id = dbModel.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().Id;
            }
            var result = await repo2.UpdateIMArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMArea4_Produksi()
        {
            string testName = GetCurrentMethod() + "UpdateIMArea4Produksi";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModelBefore();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelForUpdateAfter2();
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.Id = dbModel.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().Id;
            }
            model.SetDestinationArea("PRODUKSI", "", "");
            var result = await repo2.UpdateIMArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteTransitArea()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeleteTransitArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateTransitArea()
        {
            string testName = GetCurrentMethod() + "UpdateTransitArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();

            var result = await repo2.UpdateTransitArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateTransitArea2()
        {
            string testName = GetCurrentMethod() + "UpdateTransitArea2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModelForUpdateBefore();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelForUpdateAfter();
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.Id = dbModel.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().Id;
            }
            var result = await repo2.UpdateTransitArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdatePackingArea()
        {
            string testName = GetCurrentMethod() + "UpdatePackingArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.RestorePacking(It.IsAny<string>(), It.IsAny<List<PackingData>>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdatePackingFromOut(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
                .ReturnsAsync(new Tuple<int, List<PackingData>>(1, new List<PackingData>()));

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);


            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();

            var result = await repo2.UpdatePackingArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdatePackingArea2()
        {
            string testName = GetCurrentMethod() + "UpdatePackingArea2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.RestorePacking(It.IsAny<string>(), It.IsAny<List<PackingData>>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdatePackingFromOut(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
                .ReturnsAsync(new Tuple<int, List<PackingData>>(1, new List<PackingData>()));

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModelForUpdateBefore();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelForUpdateAfter();
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var packingData = new List<PackingData>()
                {
                    new PackingData()
                    {
                        Id = 1,
                        Balance = 10
                    }
                };
                string jsonPackingData = JsonConvert.SerializeObject(packingData);
                item.Id = dbModel.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().Id;
                item.SetProductPackingId(item.ProductPackingId + 1, "", "");
                item.SetProductPackingCode(item.ProductPackingCode + "01", "", "");
                item.SetFabricPackingId(item.FabricPackingId + 1, "", "");
                item.SetDescription(item.Description + "ss", "", "");
                item.PrevSppInJson = jsonPackingData;
            }
            var result = await repo2.UpdatePackingArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteShippingArea()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = DataUtil(repo, dbContext).GetModelShippingPenjualan();
            var created = await repo.InsertAsync(data);
            var dbData = repo.ReadAll().FirstOrDefault();
            var result = await repo2.DeleteShippingArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteShippingArea_Buyer()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputSPPMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = DataUtil(repo, dbContext).GetModelShippingBuyer();
            var created = await repo.InsertAsync(data);
            var dbData = repo.ReadAll().FirstOrDefault();
            var result = await repo2.DeleteShippingArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateShipping()
        {
            string testName = GetCurrentMethod() + "UpdateShipping";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputSPPMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModelShippingPenjualan();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelShippingPenjualanAfter();

            var result = await repo2.UpdateShippingArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateShippingBuyer()
        {
            string testName = GetCurrentMethod() + "UpdateShippingBuyer";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputSPPMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModelShippingBuyer();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelShippingBuyerAfter();

            var result = await repo2.UpdateShippingArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateShippingBuyerUpdateItem()
        {
            string testName = GetCurrentMethod() + "UpdateShippingBuyerUpdateItem";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputSPPMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModelShippingBuyer();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelShippingBuyerAfter();

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.Id = dbModel.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().Id;
            }

            var result = await repo2.UpdateShippingArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteWarehouseArea()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeleteWarehouseArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeletePackingArea()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.RestorePacking(It.IsAny<string>(), It.IsAny<List<PackingData>>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeletePackingArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeletePackingArea_ToIM()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
               .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.RestorePacking(It.IsAny<string>(), It.IsAny<List<PackingData>>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            data.SetDestinationArea("INSPECTION MATERIAL", "", "");
            var result = await repo.DeletePackingArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteAdjustment()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeleteAdjustment(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteAdjustment_Packing()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            data.SetArea("PACKING", "", "");
            var result = await repo.DeleteAdjustment(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteAdjustment_TR_Kain()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            data.SetArea("TRANSIT", "", "");
            data.SetAdjItemCategory("KAIN", "", "");
            var result = await repo.DeleteAdjustment(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteAdjustment_TR_PACK()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            data.SetArea("TRANSIT", "", "");
            data.SetAdjItemCategory("PACK", "", "");
            var result = await repo.DeleteAdjustment(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteAdjustment_Shipping()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = DataUtil(repo, dbContext).GetModelShippingPenjualan();
            await repo.InsertAsync(data);
            var result = await repo.DeleteAdjustment(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateWarehouseArea()
        {
            string testName = GetCurrentMethod() + "UpdateWarehouseArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();

            var result = await repo2.UpdateWarehouseArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateWarehouseArea2()
        {
            string testName = GetCurrentMethod() + "UpdateWarehouseArea2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModelForUpdateBefore();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelForUpdateAfter();
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.Id = dbModel.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().Id;
            }
            var result = await repo2.UpdateWarehouseArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteAvalArea()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            Mock<IDyeingPrintingAreaInputRepository> inputMock = new Mock<IDyeingPrintingAreaInputRepository>();

            inputMock.Setup(s => s.RestoreAvalTransformation(It.IsAny<List<AvalData>>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = DataUtil(repo, dbContext).GetModelShippingPenjualan();
            foreach (var item in data.DyeingPrintingAreaOutputProductionOrders)
            {
                item.PrevSppInJson = "[]";
            }
            var created = await repo.InsertAsync(data);
            var dbData = repo.ReadAll().FirstOrDefault();
            var result = await repo2.DeleteAvalArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteAvalArea_Buyer()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputSPPMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = DataUtil(repo, dbContext).GetModelShippingBuyer();
            var created = await repo.InsertAsync(data);
            var dbData = repo.ReadAll().FirstOrDefault();
            var result = await repo2.DeleteAvalArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteAdjustment_Aval()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            Mock<IDyeingPrintingAreaInputRepository> inputMock = new Mock<IDyeingPrintingAreaInputRepository>();
            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaInputModel());

            inputMock.Setup(s => s.UpdateHeaderAvalTransform(It.IsAny<DyeingPrintingAreaInputModel>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);


            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var data = DataUtil(repo, dbContext).GetModelShippingPenjualan();
            await repo.InsertAsync(data);
            var result = await repo.DeleteAdjustmentAval(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateAdj_Aval()
        {
            string testName = GetCurrentMethod() + "UpdateAdj_Aval";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            Mock<IDyeingPrintingAreaInputRepository> inputMock = new Mock<IDyeingPrintingAreaInputRepository>();
            inputMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaInputModel());

            inputMock.Setup(s => s.UpdateHeaderAvalTransform(It.IsAny<DyeingPrintingAreaInputModel>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            emptyData.SetArea("GUDANG AVAL", "", "");
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();
            model.SetArea("GUDANG AVAL", "", "");
            var result = await repo2.UpdateAdjustmentDataAval(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateAdj_2_AVAL()
        {
            string testName = GetCurrentMethod() + "UpdateAdj_2_AVAL";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            Mock<IDyeingPrintingAreaInputRepository> inputMock = new Mock<IDyeingPrintingAreaInputRepository>();
            inputMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaInputModel());

            inputMock.Setup(s => s.UpdateHeaderAvalTransform(It.IsAny<DyeingPrintingAreaInputModel>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModel();
            emptyData.SetArea("SHIPPING", "", "");
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();

            data.DyeingPrintingAreaOutputProductionOrders = new List<DyeingPrintingAreaOutputProductionOrderModel>();


            var result = await repo2.UpdateAdjustmentDataAval(data.Id, data, emptyData);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateAdj_3_AVAL()
        {
            string testName = GetCurrentMethod() + "UpdateAdj_3_AVAl";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            Mock<IDyeingPrintingAreaInputRepository> inputMock = new Mock<IDyeingPrintingAreaInputRepository>();
            inputMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaInputModel());

            inputMock.Setup(s => s.UpdateHeaderAvalTransform(It.IsAny<DyeingPrintingAreaInputModel>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);

            inputSPPMock.Setup(s => s.UpdateBalanceAndRemainsWithFlagAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModel();
            emptyData.SetArea("SHIPPING", "", "");
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();

            var item = new DyeingPrintingAreaOutputProductionOrderModel();
            item.Id = 0;
            data.DyeingPrintingAreaOutputProductionOrders.Add(item);


            var result = await repo2.UpdateAdjustmentDataAval(data.Id, data, emptyData);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateAval()
        {
            string testName = GetCurrentMethod() + "UpdateAval";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            Mock<IDyeingPrintingAreaInputRepository> inputMock = new Mock<IDyeingPrintingAreaInputRepository>();
            outputSPPMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            inputMock.Setup(s => s.UpdateAvalTransformationFromOut(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(new Tuple<int, List<AvalData>>(1, new List<AvalData>()));

            inputMock.Setup(s => s.RestoreAvalTransformation(It.IsAny<List<AvalData>>()))
                .ReturnsAsync(1);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModelShippingPenjualan();
            foreach (var item in emptyData.DyeingPrintingAreaOutputProductionOrders)
            {
                item.PrevSppInJson = "[]";
            }
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelShippingPenjualanAfter();

            var result = await repo2.UpdateAvalArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateAvalBuyer()
        {
            string testName = GetCurrentMethod() + "UpdateAvalBuyer";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            Mock<IDyeingPrintingAreaInputRepository> inputMock = new Mock<IDyeingPrintingAreaInputRepository>();
            outputSPPMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            inputMock.Setup(s => s.UpdateAvalTransformationFromOut(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(new Tuple<int, List<AvalData>>(1, new List<AvalData>()));

            inputMock.Setup(s => s.RestoreAvalTransformation(It.IsAny<List<AvalData>>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputMock.Object);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModelShippingBuyer();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelShippingBuyerAfter();

            var result = await repo2.UpdateAvalArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateAvalBuyerUpdateItem()
        {
            string testName = GetCurrentMethod() + "UpdateAvalBuyerUpdateItem";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();
            Mock<IDyeingPrintingAreaInputRepository> inputMock = new Mock<IDyeingPrintingAreaInputRepository>();
            outputSPPMock.Setup(s => s.UpdateFromInputNextAreaFlagAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            inputMock.Setup(s => s.UpdateAvalTransformationFromOut(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(new Tuple<int, List<AvalData>>(1, new List<AvalData>()));

            inputMock.Setup(s => s.RestoreAvalTransformation(It.IsAny<List<AvalData>>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);
            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputMock.Object);
            var repo = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetModelShippingBuyer();

            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModelShippingBuyerAfter();

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                item.Id = dbModel.DyeingPrintingAreaOutputProductionOrders.FirstOrDefault().Id;
            }

            var result = await repo2.UpdateAvalArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }
    }
}

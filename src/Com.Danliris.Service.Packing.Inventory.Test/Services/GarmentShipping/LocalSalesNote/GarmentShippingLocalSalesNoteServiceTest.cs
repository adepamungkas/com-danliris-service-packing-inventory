﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingLocalSalesNoteRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesNoteRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingLocalSalesNoteService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingLocalSalesNoteService(serviceProvider);
        }

        protected GarmentShippingLocalSalesNoteViewModel ViewModel
        {
            get
            {
                return new GarmentShippingLocalSalesNoteViewModel
                {
                    items = new List<GarmentShippingLocalSalesNoteItemViewModel>()
                    {
                        new GarmentShippingLocalSalesNoteItemViewModel()
                    }
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingLocalSalesNoteModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingLocalSalesNoteModel("", DateTimeOffset.Now, 1, "", "", 1, "", "", "", 1, "", true, "", new List<GarmentShippingLocalSalesNoteItemModel>());

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var items = new List<GarmentShippingLocalSalesNoteItemModel>() { new GarmentShippingLocalSalesNoteItemModel(1, "", "", 1, 1, "", 1) };
            var model = new GarmentShippingLocalSalesNoteModel("", DateTimeOffset.Now, 1, "", "", 1, "", "", "", 1, "", true, "", items);

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingLocalSalesNoteModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
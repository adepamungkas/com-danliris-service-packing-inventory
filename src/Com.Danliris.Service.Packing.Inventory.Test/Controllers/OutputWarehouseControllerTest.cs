﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse.InputSPPWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers.DyeingPrintingAreaOutput;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using IndexViewModel = Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse.IndexViewModel;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class OutputWarehouseControllerTest
    {
        private OutputWarehouseController GetController(IOutputWarehouseService service, IIdentityProvider identityProvider, IValidateService validateService)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new OutputWarehouseController(service, identityProvider, validateService)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = claimPrincipal.Object

                    }
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            return controller;
        }

        private ServiceValidationException GetServiceValidationExeption()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            List<ValidationResult> validationResults = new List<ValidationResult>()
            {
                new ValidationResult("message",new string[1]{ "A" }),
                new ValidationResult("{}",new string[1]{ "B" })
            };
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(ViewModel, serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private OutputWarehouseViewModel ViewModel
        {
            get
            {
                return new OutputWarehouseViewModel()
                {
                    Id = 1,
                    Area = "GUDANG JADI",
                    BonNo = "TR.GJ.20.001",
                    Date = DateTimeOffset.UtcNow,
                    DestinationArea = "SHIPPING",
                    HasNextAreaDocument = false,
                    Shift = "PAGI",
                    InputWarehouseId = 12,
                    Group = "A",
                    WarehousesProductionOrders = new List<OutputWarehouseProductionOrderViewModel>()
                    {
                        new OutputWarehouseProductionOrderViewModel()
                        {
                            Id = 1,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
                            CartNo = "5-11",
                            PackingInstruction = "a",
                            Construction = "a",
                            Unit = "a",
                            Buyer = "a",
                            Color = "a",
                            Motif = "a",
                            UomUnit = "a",
                            Remark = "a",
                            Grade = "a",
                            Status = "a",
                            Balance = 50,
                            PreviousBalance = 100,
                            InputId = 2,
                            ProductionOrderNo = "asd",
                            HasNextAreaDocument = false,
                            Material = "a",
                            MtrLength = 10,
                            YdsLength = 10,
                            Quantity = 10,
                            PackagingType = "s",
                            PackagingUnit = "a",
                            PackagingQty = 10,
                            QtyOrder = 10
                        }
                    }
                };
            }
        }

        [Fact]
        public void Should_Validator_Success()
        {
            var dataUtil = new OutputWarehouseViewModel();
            var validator = new OutputWarehouseValidator();
            var result = validator.Validate(dataUtil);
            Assert.NotEqual(0, result.Errors.Count);
        }

        [Fact]
        public async Task Should_Success_Post()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputWarehouseViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputWarehouseViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;
            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotValid_Post()
        {
            var dataUtil = new OutputWarehouseViewModel();
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputWarehouseViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputWarehouseViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;
            var controller = GetController(service, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }


        [Fact]
        public async Task Should_Exception_Post()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputWarehouseViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputWarehouseViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;
            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_ServiceValidationException_Post()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Create(It.IsAny<OutputWarehouseViewModel>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputWarehouseViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Post(dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetById()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ReturnsAsync(ViewModel);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_GetById()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_Get()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<IndexViewModel>(new List<IndexViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Get()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Success_GetListBon()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Read(It.IsAny<string>()))
                .Returns(new ListResult<IndexViewModel>(new List<IndexViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetListBon();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetListBon()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Read(It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetListBon();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Success_GetInputSppWarehouseItemList()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetInputSppWarehouseItemListV2(It.IsAny<long>()))
                .Returns(new List<InputSppWarehouseViewModel>());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.NewGetProductionOrdersv2();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetInputSppWarehouseItemList()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetInputSppWarehouseItemListV2(It.IsAny<long>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.NewGetProductionOrdersv2();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetProductionOrdersv2WithBonID()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetInputSppWarehouseItemList(1))
                .Returns(new List<InputSppWarehouseViewModel>());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetProductionOrdersv2(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetProductionOrdersv2WithBonID()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetInputSppWarehouseItemList(1)).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetProductionOrdersv2(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetOutputProductionOrdersv2WithBonID()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetOutputSppWarehouseItemListAsync(1))
                .ReturnsAsync(new List<InputSppWarehouseViewModel>());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetOutputProductionOrdersv2(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_GetOutputProductionOrdersv2WithBonID()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetOutputSppWarehouseItemListAsync(1)).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetOutputProductionOrdersv2(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Success_GetProductionOrders()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetInputWarehouseProductionOrders())
                .Returns(new List<InputWarehouseProductionOrderCreateViewModel>() { });
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetProductionOrders();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetProductionOrders()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetInputWarehouseProductionOrders()).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetProductionOrders();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_GetWarehouseAreaNoteExcel()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetExcel(1);

            Assert.NotNull(response);
        }
        [Fact]
        public void Should_Success_GetWarehouseAreaNoteExcelAll()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GenerateExcelAll(It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<int>()))
                .Returns(new MemoryStream());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetExcelAll("7");

            Assert.NotNull(response);
        }
        [Fact]
        public void Should_Exception_GetWarehouseAreaNoteExcelAll()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GenerateExcelAll(It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetExcelAll("7");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));

        }
        [Fact]
        public async Task Should_Exception_GetWarehouseAreaNoteExcel()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GenerateExcel(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.GetExcel(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async void Should_Exception_Delete()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Delete(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public async void Should_Success_Delete()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(1);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;
            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Delete(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetAdjProductionOrders()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetDistinctAllProductionOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<AdjWarehouseProductionOrderViewModel>(new List<AdjWarehouseProductionOrderViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAdjProductionOrder();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetAdjProductionOrders()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetDistinctAllProductionOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetAdjProductionOrder();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Put()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputWarehouseViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputWarehouseViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(1, dataUtil);

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotValid_Put()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputWarehouseViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputWarehouseViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            controller.ModelState.AddModelError("test", "test");
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(1, dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_ValidateExcpetion_Put()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputWarehouseViewModel>())).ReturnsAsync(1);
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputWarehouseViewModel>()))
                .Throws(GetServiceValidationExeption());
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(1, dataUtil);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Put()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<OutputWarehouseViewModel>())).Throws(new Exception("mess"));
            var service = serviceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<OutputWarehouseViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = await controller.Put(1, dataUtil);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetDistinctProductionOrders()
        {
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetDistinctProductionOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<InputWarehouseProductionOrderCreateViewModel>(new List<InputWarehouseProductionOrderCreateViewModel>(), 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetDistinctProductionOrder();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetDistinctProductionOrders()
        {
            var dataUtil = ViewModel;
            //v
            var serviceMock = new Mock<IOutputWarehouseService>();
            serviceMock.Setup(s => s.GetDistinctProductionOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            //controller.ModelState.IsValid == false;
            var response = controller.GetDistinctProductionOrder();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}

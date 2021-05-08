﻿using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.Master
{
    public class MaterialConstructionRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, MaterialConstructionRepository, MaterialConstructionModel, MaterialConstructionDataUtil>
    {
        private const string ENTITY = "MaterialConstruction";
        public MaterialConstructionRepositoryTest() : base(ENTITY)
        {

        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new MaterialConstructionRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetDbSet();

            Assert.NotEmpty(result);
        }

        [Fact]
        public virtual async Task Should_Success_MultipleInsert()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new MaterialConstructionRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = DataUtil(repo, dbContext).GetModel();
            var result = await repo.MultipleInsertAsync(new List<MaterialConstructionModel>() { data });
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_GetCodeByType()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new MaterialConstructionRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetCodeByType(data.Type);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Zero_GetCodeByType()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new MaterialConstructionRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetCodeByType("");

            Assert.Equal(0, result);
        }
    }
}

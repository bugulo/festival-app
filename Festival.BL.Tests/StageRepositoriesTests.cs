using System;
using System.Linq;
using Festival.BL.Mappers;
using Festival.BL.Models;
using Festival.BL.Repositories;
using Festival.DAL.Entities;
using Festival.DAL.Factories;
using Festival.DAL.Seeds;
using Xunit;

namespace Festival.BL.Tests
{
    public class StageRepositoriesTests : IDisposable
    {
        private readonly StageRepository _stageRepositorySUT;
        private readonly InMemoryDbContextFactory _dbContextFactory;

        public StageRepositoriesTests()
        {
            _dbContextFactory = new InMemoryDbContextFactory(nameof(StageRepositoriesTests));
            
            TestSeed.Seed(_dbContextFactory);

            using var dbx = _dbContextFactory.Create();
            
            dbx.Database.EnsureCreated();

            _stageRepositorySUT = new StageRepository(_dbContextFactory);
        }


        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            using var dbxAssert = _dbContextFactory.Create();
            dbxAssert.Database.EnsureDeleted();

            var model = new StageDetailModel
            {
                Name = "Urpiner stage",
                Description = "Main stage",
                PhotoURL = "https://www.interez.sk/wp-content/uploads/2017/07/pohoda3den-38-770x514.jpg?x68992",
            };

            var returnedModel = _stageRepositorySUT.InsertOrUpdate(model);

            Assert.NotNull(returnedModel);
        }

        [Fact]
        public void NewStage_InsertOrUpdate_StageAdded()
        {
            //Arrange
            var stage = new StageDetailModel
            {
                Name = "Budis stage",
                Description = "stage with seats",
                PhotoURL = "https://m.smedata.sk/api-media/media/image/sme/4/26/2625394/2625394_1200x800.jpeg?rev=2",
            };

            //Act
            stage = _stageRepositorySUT.InsertOrUpdate(stage);

            //Assert
            using var dbxAssert = _dbContextFactory.Create();
            var stageFromDb = dbxAssert.Stages.Single(i => i.Id == stage.Id);
            Assert.Equal(stage, StageMapper.MapToDetailModel(stageFromDb));
        }

        [Fact]
        public void UpdateStage_InsertOrUpdate_SlotAdded()
        {
            //Arrange
            using var dbxAssert = _dbContextFactory.Create();

            var stageEntity = dbxAssert.Stages.Single(i => i.Id == TestSeed.StageEntity1.Id);

            var stage = StageMapper.MapToDetailModel(stageEntity);

            stage.Name = "Stage test";

            //Act
            stage = _stageRepositorySUT.InsertOrUpdate(stage);

            //Assert
            using var ndbxAssert = _dbContextFactory.Create();
            var stageFromDb = ndbxAssert.Stages.Single(i => i.Id == stage.Id);
            Assert.Equal(stage, StageMapper.MapToDetailModel(stageFromDb));
        }

        [Fact]
        public void GetAll_Single_Stage()
        {
            using var dbxAssert = _dbContextFactory.Create();
            StageEntity dbStage = dbxAssert.Stages.Single(i => i.Id == TestSeed.StageEntity1.Id);

            var stage = _stageRepositorySUT
                .GetAll()
                .Single(i => i.Id == dbStage.Id);

            Assert.Equal(StageMapper.MapToListModel(dbStage), stage);
        }

        [Fact]
        public void GetById_Stage()
        {
            using var dbxAssert = _dbContextFactory.Create();
            StageEntity dbStage = dbxAssert.Stages.Single(i => i.Id == TestSeed.StageEntity1.Id);
         
            var stage = _stageRepositorySUT.GetById(dbStage.Id);

            Assert.Equal(StageMapper.MapToDetailModel(dbStage), stage);
        }

        [Fact]
        public void DeleteById_Stage_Deleted()
        {
            using var dbxAssert = _dbContextFactory.Create();
            StageEntity dbStage = dbxAssert.Stages.Single(i => i.Id == TestSeed.StageEntity1.Id);

            _stageRepositorySUT.Delete(dbStage.Id);

            Assert.False(dbxAssert.Stages.Any(i => i.Id == dbStage.Id));
        }
        

        public void Dispose()
        {
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureDeleted();
        }
    }
}

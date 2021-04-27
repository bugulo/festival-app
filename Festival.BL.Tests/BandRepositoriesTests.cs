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
    public class BandRepositoriesTests : IDisposable
    {
        private readonly BandRepository _bandRepositorySUT;
        private readonly InMemoryDbContextFactory _dbContextFactory;

        public BandRepositoriesTests()
        {
            _dbContextFactory = new InMemoryDbContextFactory(nameof(BandRepositoriesTests));
            TestSeed.Seed(_dbContextFactory);

            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _bandRepositorySUT = new BandRepository(_dbContextFactory);
        }


        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            using var dbxAssert = _dbContextFactory.Create();
            dbxAssert.Database.EnsureDeleted();

            var model = new BandDetailModel
            {
                Name = "Test band",
                Genre = "Rock",
                PhotoURL = "https://upload.wikimedia.org/wikipedia/commons/4/40/Kombii_1.jpg",
                Country = DAL.Enums.Country.Slovakia,
                Description = "Test description"
            };

            var returnedModel = _bandRepositorySUT.InsertOrUpdate(model);

            Assert.NotNull(returnedModel);
        }

        [Fact]
        public void NewBand_InsertOrUpdate_BandAdded()
        {
            //Arrange
            var band = new BandDetailModel()
            {
                Name = "Kombii",
                Genre = "Pop rock",
                PhotoURL = "https://upload.wikimedia.org/wikipedia/commons/4/40/Kombii_1.jpg",
                Country = DAL.Enums.Country.Poland,
                Description = "Kombii is a Polish pop rock band created in 2003 by three former members of the group Kombi."
            };

            //Act
            band = _bandRepositorySUT.InsertOrUpdate(band);

            //Assert
            using var dbxAssert = _dbContextFactory.Create();
            var bandFromDb = dbxAssert.Bands.Single(i => i.Id == band.Id);
            Assert.Equal(band, BandMapper.MapToDetailModel(bandFromDb));
        }

        [Fact]
        public void UpdateBand_InsertOrUpdate_BandAdded()
        {
            //Arrange
            using var dbxAssert = _dbContextFactory.Create();

            var bandEntity = dbxAssert.Bands.Single(i => i.Id == TestSeed.BandEntity1.Id);

            var band = BandMapper.MapToDetailModel(bandEntity);

            band.Description = "Updated test description";

            //Act
            band = _bandRepositorySUT.InsertOrUpdate(band);

            //Assert
            using var ndbxAssert = _dbContextFactory.Create();
            var slotFromDb = ndbxAssert.Bands.Single(i => i.Id == band.Id);
            Assert.Equal(band, BandMapper.MapToDetailModel(slotFromDb));
        }

        [Fact]
        public void GetAll_Single_Band()
        {
            using var dbxAssert = _dbContextFactory.Create();
            BandEntity dbBand = dbxAssert.Bands.Single(i => i.Id == TestSeed.BandEntity1.Id);

            var band = _bandRepositorySUT
                .GetAll()
                .Single(i => i.Id == dbBand.Id);

            Assert.Equal(BandMapper.MapToListModel(dbBand), band);
        }

        [Fact]
        public void GetById_Band()
        {
            using var dbxAssert = _dbContextFactory.Create();
            BandEntity dbBand = dbxAssert.Bands.Single(i => i.Id == TestSeed.BandEntity1.Id);
         
            var band = _bandRepositorySUT.GetById(dbBand.Id);

            Assert.Equal(BandMapper.MapToDetailModel(dbBand), band);
        }

        [Fact]
        public void DeleteById_Band_Deleted()
        {
            using var dbxAssert = _dbContextFactory.Create();
            BandEntity dbBand = dbxAssert.Bands.Single(i => i.Id == TestSeed.BandEntity1.Id);

            _bandRepositorySUT.Delete(dbBand.Id);

            Assert.False(dbxAssert.Bands.Any(i => i.Id == dbBand.Id));
        }
        

        public void Dispose()
        {
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureDeleted();
        }
    }
}

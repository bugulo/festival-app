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
    public class SlotRepositoriesTests : IDisposable
    {
        private readonly SlotRepository _slotRepositorySUT;
        private readonly InMemoryDbContextFactory _dbContextFactory;

        public SlotRepositoriesTests()
        {
            _dbContextFactory = new InMemoryDbContextFactory(nameof(SlotRepositoriesTests));
            TestSeed.Seed(_dbContextFactory);

            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _slotRepositorySUT = new SlotRepository(_dbContextFactory);
        }


        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            using var dbxAssert = _dbContextFactory.Create();
            dbxAssert.Database.EnsureDeleted();

            var model = new SlotDetailModel
            {
                StartAt = new DateTime(2021, 7, 6, 19, 30, 0),
                FinishAt = new DateTime(2021, 7, 6, 20, 10, 0),
            };

            var returnedModel = _slotRepositorySUT.InsertOrUpdate(model);

            Assert.NotNull(returnedModel);
        }

        [Fact]
        public void IsSlotAvailable_Unavailable()
        {
            using var dbxAssert = _dbContextFactory.Create();
            
            var model = new SlotDetailModel
            {
                StartAt = new DateTime(2021, 7, 6, 15, 40, 0),
                FinishAt = new DateTime(2021, 7, 6, 16, 40, 0),

                BandId = TestSeed.BandEntity1.Id,
                StageId = TestSeed.StageEntity2.Id
            };

            var returnedModel = _slotRepositorySUT.InsertOrUpdate(model);

            Assert.Null(returnedModel);
        }

        [Fact]
        public void NewSlot_InsertOrUpdate_SlotAdded()
        {
            //Arrange
            using var dbxAssert = _dbContextFactory.Create();

            var slot = new SlotDetailModel()
            {
                StartAt = new DateTime(2021, 7, 6, 20, 20, 0),
                FinishAt = new DateTime(2021, 7, 6, 21, 35, 0),

                BandId = TestSeed.BandEntity1.Id,
                StageId = TestSeed.StageEntity1.Id
            };

            //Act
            slot = _slotRepositorySUT.InsertOrUpdate(slot);

            //Assert
            var slotFromDb = dbxAssert.Slots.Single(i => i.Id == slot.Id);
            Assert.Equal(slot, SlotMapper.MapToDetailModel(slotFromDb));
        }

        [Fact]
        public void UpdateSlot_InsertOrUpdate_SlotAdded()
        {
            //Arrange
            using var dbxAssert = _dbContextFactory.Create();

            var slotEntity = dbxAssert.Slots.Single(i => i.Id == TestSeed.SlotEntity1.Id);

            var slot = SlotMapper.MapToDetailModel(slotEntity);

            slot.StartAt = new DateTime(2021, 7, 6, 15, 40, 0);
            slot.FinishAt = new DateTime(2021, 7, 6, 16, 40, 0);

            //Act
            slot = _slotRepositorySUT.InsertOrUpdate(slot);

            //Assert
            using var ndbxAssert = _dbContextFactory.Create();
            var slotFromDb = ndbxAssert.Slots.Single(i => i.Id == slot.Id);
            Assert.Equal(slot, SlotMapper.MapToDetailModel(slotFromDb));
        }

        [Fact]
        public void GetAll_Single_Slot()
        {
            using var dbxAssert = _dbContextFactory.Create();
            SlotEntity dbSlot = dbxAssert.Slots.Single(i => i.Id == TestSeed.SlotEntity1.Id);

            var slot = _slotRepositorySUT
                .GetAll()
                .Single(i => i.Id == dbSlot.Id);

            Assert.Equal(SlotMapper.MapToListModel(dbSlot), slot);
        }

        [Fact]
        public void GetById_Slot()
        {
            using var dbxAssert = _dbContextFactory.Create();
            SlotEntity dbSlot = dbxAssert.Slots.Single(i => i.Id == TestSeed.SlotEntity1.Id);

            var slot = _slotRepositorySUT.GetById(dbSlot.Id);

            Assert.Equal(SlotMapper.MapToDetailModel(dbSlot), slot);
        }

        [Fact]
        public void DeleteById_Slot_Deleted()
        {
            using var dbxAssert = _dbContextFactory.Create();
            SlotEntity dbSlot = dbxAssert.Slots.Single(i => i.Id == TestSeed.SlotEntity1.Id);

            _slotRepositorySUT.Delete(dbSlot.Id);

            Assert.False(dbxAssert.Slots.Any(i => i.Id == dbSlot.Id));
        }


        public void Dispose()
        {
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureDeleted();
        }
    }
}

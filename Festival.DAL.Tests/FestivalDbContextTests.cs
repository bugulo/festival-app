using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Xunit;

using Festival.DAL.Enums;
using Festival.DAL.Seeds;
using Festival.DAL.Entities;
using Festival.DAL.Factories;

namespace Festival.DAL.Tests
{
    public class FestivalDbContextTests : IDisposable
    {
        private readonly InMemoryDbContextFactory _dbContextFactory;
        private readonly FestivalDbContext _dbContext;

        public FestivalDbContextTests()
        {
            _dbContextFactory = new InMemoryDbContextFactory(nameof(FestivalDbContextTests));
            TestSeed.Seed(_dbContextFactory);

            _dbContext = _dbContextFactory.Create();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void AddBandTest()
        {
            var band = new BandEntity { 
                Name = "Example Band", 
                Genre = "Rock", 
                PhotoURL = "https://www.somewebsite.com/picture_band1.jpg", 
                Country = Country.Germany, 
                Description = "Example Band are rock band formed in Frankfurt in 2021", 
                ProgramDescription = "Example Band is going to perform every day on every stage",
            };

            _dbContext.Bands.Add(band);
            _dbContext.SaveChanges();

            using var dbx = _dbContextFactory.Create();
            var result = dbx.Bands.FirstOrDefault(x => x.Id == band.Id);
            Assert.Equal(band, result, BandEntity.BandComparer);
        }

        [Fact]
        public void AddStageEntity()
        {
            var stage = new StageEntity { 
                Name = "North East Stage", 
                Description = "The biggest stage the world has ever seen", 
                PhotoURL = "https://www.somewebsite.com/picture_stage1.jpg"
            };

            _dbContext.Stages.Add(stage);
            _dbContext.SaveChanges();

            using var dbx = _dbContextFactory.Create();
            var result = dbx.Stages.FirstOrDefault(x => x.Id == stage.Id);
            Assert.Equal(stage, result, StageEntity.StageComparer);
        }

        [Fact]
        public void AddSlotEntity()
        {
            var slot = new SlotEntity {
                StartAt = DateTime.Now,
                FinishAt = DateTime.Now.AddHours(2),

                BandId = TestSeed.BandEntity2.Id,
                StageId = TestSeed.StageEntity2.Id
            };

            _dbContext.Slots.Add(slot);
            _dbContext.SaveChanges();

            using var dbx = _dbContextFactory.Create();

            var slotResult = dbx.Slots.Include(x => x.Band).Include(x => x.Stage).First(x => x.Id == slot.Id);

            Assert.Equal(slotResult.Band, TestSeed.BandEntity2, BandEntity.BandWithoutSlotsComparer);
            Assert.Equal(slotResult.Stage, TestSeed.StageEntity2, StageEntity.StageWithoutSlotsComparer);
        }

        [Fact]
        public void DeleteBandTest()
        {
            var bandEntity = _dbContext.Bands.Include(x => x.Slots).First(x => x.Id == TestSeed.BandEntity1.Id);

            _dbContext.Bands.Remove(bandEntity);
            _dbContext.SaveChanges();

            using var dbx = _dbContextFactory.Create();
            var bandResult = dbx.Bands.FirstOrDefault(x => x.Id == bandEntity.Id);
            var slotCount = dbx.Slots.Count(x => x.BandId == bandEntity.Id);

            Assert.Null(bandResult);
            Assert.Equal(0, slotCount);
        }

        [Fact]
        public void DeleteStageTest()
        {
            var stageEntity = _dbContext.Stages.Include(x => x.Slots).First(x => x.Id == TestSeed.StageEntity1.Id);

            _dbContext.Stages.Remove(stageEntity);
            _dbContext.SaveChanges();

            using var dbx = _dbContextFactory.Create();
            var stageResult = dbx.Stages.FirstOrDefault(x => x.Id == stageEntity.Id);
            var slotCount = dbx.Slots.Count(x => x.BandId == stageEntity.Id);

            Assert.Null(stageResult);
            Assert.Equal(0, slotCount);
        }

        [Fact]
        public void DeleteSlotTest()
        {
            var slotEntity = _dbContext.Slots.First(x => x.Id == TestSeed.SlotEntity1.Id);

            _dbContext.Slots.Remove(slotEntity);
            _dbContext.SaveChanges();

            using var dbx = _dbContextFactory.Create();
            var slotResult = dbx.Slots.FirstOrDefault(x => x.Id == slotEntity.Id);
            var bandResult = dbx.Bands.FirstOrDefault(x => x.Id == slotEntity.BandId);
            var stageResult = dbx.Stages.FirstOrDefault(x => x.Id == slotEntity.StageId);

            Assert.Null(slotResult);
            Assert.NotNull(bandResult);
            Assert.NotNull(stageResult);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }
}

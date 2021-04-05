using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

using Festival.DAL.Enums;
using Festival.DAL.Entities;

namespace Festival.DAL.Tests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class FestivalDbContextTests : IClassFixture<FestivalDbContextClassFixture>
    {
        private readonly FestivalDbContextClassFixture _fixture;

        private readonly ITestOutputHelper _output;

        public FestivalDbContextTests(FestivalDbContextClassFixture classFixture, ITestOutputHelper output)
        {
            _fixture = classFixture;
            _output = output;
        }

        [Fact, Priority(0)]
        public void AddStageEntity()
        {
            var stage = new StageEntity { 
                Name = "North East Stage", 
                Description = "The biggest stage the world has ever seen", 
                PhotoURL = "https://www.somewebsite.com/picture_stage1.jpg"
            };

            _fixture.DbContext.Stages.Add(stage);
            _fixture.DbContext.SaveChanges();

            var dbContext = _fixture.DbContextFactory.Create();
            var result = dbContext.Stages.FirstOrDefault(x => x.Id == stage.Id);
            Assert.Equal(stage, result, StageEntity.StageComparer);
        }

        [Fact, Priority(1)]
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

            _fixture.DbContext.Bands.Add(band);
            _fixture.DbContext.SaveChanges();

            var dbContext = _fixture.DbContextFactory.Create();
            var result = dbContext.Bands.FirstOrDefault(x => x.Id == band.Id);
            Assert.Equal(band, result, BandEntity.BandComparer);
        }

        [Fact, Priority(2)]
        public void AddSlotEntity()
        {
            var slot = new SlotEntity {
                StartAt = DateTime.Now,
                FinishAt = DateTime.Now.AddHours(2),

                Band = _fixture.DbContext.Bands.First(),
                Stage = _fixture.DbContext.Stages.First()
            };

            _fixture.DbContext.Slots.Add(slot);
            _fixture.DbContext.SaveChanges();

            var dbContext = _fixture.DbContextFactory.Create();

            var slotResult = dbContext.Slots.First(x => x.Id == slot.Id);
            var bandResult = dbContext.Bands.Include(x => x.Slots).FirstOrDefault(x => x.Id == slot.Band.Id);
            var stageResult = dbContext.Stages.Include(x => x.Slots).FirstOrDefault(x => x.Id == slot.Stage.Id);

            Assert.Equal(slot, slotResult, SlotEntity.SlotComparer);
            Assert.Equal(bandResult, slot.Band, BandEntity.BandComparer);
            Assert.Equal(stageResult, slot.Stage, StageEntity.StageComparer);
        }

        [Fact, Priority(3)]
        public void DeleteBandTest()
        {
            _fixture.DbContext.Bands.Remove(_fixture.DbContext.Bands.First());
            _fixture.DbContext.SaveChanges();

            var dbContext = _fixture.DbContextFactory.Create();
            Assert.Equal(0, dbContext.Bands.Count());
            Assert.Equal(1, dbContext.Stages.Count());
            Assert.Equal(0, dbContext.Slots.Count());
        }

        [Fact, Priority(4)]
        public void DeleteStageTest()
        {
            _fixture.DbContext.Stages.Remove(_fixture.DbContext.Stages.First());
            _fixture.DbContext.SaveChanges();

            var dbContext = _fixture.DbContextFactory.Create();
            Assert.Equal(0, dbContext.Bands.Count());
            Assert.Equal(0, dbContext.Stages.Count());
            Assert.Equal(0, dbContext.Slots.Count());
        }
    }
}

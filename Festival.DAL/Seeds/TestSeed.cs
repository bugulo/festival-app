using System;

using Festival.DAL.Entities;
using Festival.DAL.Interfaces;

namespace Festival.DAL.Seeds
{
    public static class TestSeed
    {
        public static readonly BandEntity BandEntity1 = new BandEntity {
            Id = Guid.NewGuid(),
            Name = "Test Band 1", 
            Genre = "Test Genre 1", 
            PhotoURL = "https://www.test.com/photo1.jpg", 
            Country = Enums.Country.Germany, 
            Description = "Test description 1", 
            ProgramDescription = "Test program description 1"
        };

        public static readonly BandEntity BandEntity2 = new BandEntity {
            Id = Guid.NewGuid(),
            Name = "Test Band 2", 
            Genre = "Test Genre 2", 
            PhotoURL = "https://www.test.com/photo2.jpg", 
            Country = Enums.Country.Slovakia, 
            Description = "Test description 2", 
            ProgramDescription = "Test program description 2"
        };

        public static readonly StageEntity StageEntity1 = new StageEntity {
            Id = Guid.NewGuid(),
            Name = "Test Stage 1", 
            Description = "Test description 1", 
            PhotoURL = "https://www.test.com/stage1.jpg"
        };

        public static readonly StageEntity StageEntity2 = new StageEntity {
            Id = Guid.NewGuid(),
            Name = "Test Stage 2", 
            Description = "Test description 2", 
            PhotoURL = "https://www.test.com/stage2.jpg"
        };

        public static readonly SlotEntity SlotEntity1 = new SlotEntity
        {
            Id = Guid.NewGuid(),
            StartAt = new DateTime(2021, 7, 6, 15, 10, 0),
            FinishAt = new DateTime(2021, 7, 6, 16, 10, 0),

            Band = BandEntity1,
            Stage = StageEntity1
        };

        public static void Seed(IDbContextFactory dbContextFactory)
        {
            using var dbx = dbContextFactory.Create();
            
            dbx.Bands.Add(BandEntity1);
            dbx.Bands.Add(BandEntity2);

            dbx.Stages.Add(StageEntity1);
            dbx.Stages.Add(StageEntity2);

            dbx.Slots.Add(SlotEntity1);

            dbx.SaveChanges();
        }
    }
}
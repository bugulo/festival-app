using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.BL.Models;

namespace Festival.App.Wrappers
{
    public class SlotWrapper : ModelWrapper<SlotDetailModel>
    {
        public SlotWrapper(SlotDetailModel model)
            : base(model)
        {
        }

        public DateTime StartAt 
        { 
            get => GetValue<DateTime>(); 
            set => SetValue(value); 
        }

        public DateTime FinishAt 
        { 
            get => GetValue<DateTime>(); 
            set => SetValue(value); 
        }

        public Guid BandId 
        { 
            get => GetValue<Guid>(); 
            set => SetValue(value); 
        }

        public Guid StageId 
        { 
            get => GetValue<Guid>(); 
            set => SetValue(value); 
        }

        public string BandName 
        { 
            get => GetValue<string>(); 
            set => SetValue(value); 
        }

        public string BandDescription 
        { 
            get => GetValue<string>(); 
            set => SetValue(value); 
        }

        public string BandPhotoURL 
        { 
            get => GetValue<string>(); 
            set => SetValue(value); 
        }

        public string StageName 
        { 
            get => GetValue<string>(); 
            set => SetValue(value); 
        }

        public string StageDescription 
        { 
            get => GetValue<string>(); 
            set => SetValue(value); 
        }

        public static implicit operator SlotWrapper(SlotDetailModel detailModel)
            => new(detailModel);

        public static implicit operator SlotDetailModel(SlotWrapper wrapper)
            => wrapper.Model;

    }
}

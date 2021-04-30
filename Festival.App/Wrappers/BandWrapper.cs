using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Festival.BL.Models;
using Festival.DAL.Enums;


namespace Festival.App.Wrappers
{
    public class BandWrapper : ModelWrapper<BandDetailModel>
    {
        public BandWrapper(BandDetailModel model)
            : base(model)
        {
        }

        public string Name 
        { 
            get => GetValue<string>(); 
            set => SetValue(value); 
        }

        public string Genre 
        { 
            get => GetValue<string>(); 
            set => SetValue(value); 
        }

        public string PhotoURL 
        { 
            get => GetValue<string>(); 
            set => SetValue(value); 
        }

        public Country Country 
        { 
            get => GetValue<Country>(); 
            set => SetValue(value); 
        }

        public string Description 
        { 
            get => GetValue<string>(); 
            set => SetValue(value); 
        }

        public static implicit operator BandWrapper(BandDetailModel detailModel)
            => new(detailModel);

        public static implicit operator BandDetailModel(BandWrapper wrapper)
            => wrapper.Model;

    }
}

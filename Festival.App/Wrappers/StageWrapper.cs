using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.BL.Models;

namespace Festival.App.Wrappers
{
    public class StageWrapper : ModelWrapper<StageDetailModel>
    {
        public StageWrapper(StageDetailModel model)
            : base(model)
        {
        }

        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Description
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string PhotoURL
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public static implicit operator StageWrapper(StageDetailModel detailModel)
            => new(detailModel);

        public static implicit operator StageDetailModel(StageWrapper wrapper)
            => wrapper.Model;
    }
}

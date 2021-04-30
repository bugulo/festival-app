using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.BL.Models;

namespace Festival.App.Messages
{
    public class UpdatedMessage<T> : Message<T>
        where T : IModel
    {
    }
}

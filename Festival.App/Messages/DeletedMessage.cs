using System;
using Festival.BL.Models;

namespace Festival.App.Messages
{
    public class DeletedMessage<T> : Message<T>
        where T : IModel
    {
    }
}
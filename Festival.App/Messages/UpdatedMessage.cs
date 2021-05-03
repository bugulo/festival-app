using Festival.BL.Models;

namespace Festival.App.Messages
{
    public class UpdatedMessage<T> : Message<T>
        where T : IModel
    {
    }
}

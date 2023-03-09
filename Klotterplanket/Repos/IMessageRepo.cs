using Klotterplanket.Models;

namespace Klotterplanket.Repos
{
    public interface IMessageRepo
    {
        public Task<List<MessageModel>> GetAllMessages();

        public Task AddMessage(MessageModel message);
    }
}

using Klotterplanket.Data;
using Klotterplanket.Models;
using Microsoft.EntityFrameworkCore;

namespace Klotterplanket.Repos
{
    public class MessageRepo : IMessageRepo
    {
        private readonly AppDbContext context;

        public MessageRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddMessage(MessageModel message)
        {

           await context.Messages.AddAsync(message);
           await context.SaveChangesAsync();
        }

        public async Task<List<MessageModel>> GetAllMessages()
        {
            return await context.Messages.ToListAsync();
        }
    }
}

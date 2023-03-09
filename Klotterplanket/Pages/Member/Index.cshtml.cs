using Klotterplanket.Models;
using Klotterplanket.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Klotterplanket.Pages.Member
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IMessageRepo messageRepo;
        private string username;

        [BindProperty]
        [Required]
        public string? NewMessage { get; set; }
        public List<MessageModel> MessageList { get; set; }

        public IndexModel(SignInManager<IdentityUser> signInManager, IMessageRepo messageRepo)
        {
            this.signInManager = signInManager;
            this.messageRepo = messageRepo;

        }
        public async Task OnGet()
        {

            MessageList = await messageRepo.GetAllMessages();
        }

        public async Task<IActionResult> OnPost()
        {
            IdentityUser? currentUser = await signInManager.UserManager.GetUserAsync(HttpContext.User);

            if (currentUser != null)
            {
                username = currentUser.UserName;
            }

            MessageModel messageToAdd = new MessageModel()
            {
                Message = NewMessage,
                Date = DateTime.Now,
                Username = username
            };

            if (messageToAdd.Message != null)
            {
                await messageRepo.AddMessage(messageToAdd);
            }

            MessageList = await messageRepo.GetAllMessages();

            ModelState.Clear();
            NewMessage = String.Empty;
            return Page();
        }
    }
}

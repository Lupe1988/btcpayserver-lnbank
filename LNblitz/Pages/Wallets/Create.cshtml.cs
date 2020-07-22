using System;
using System.Threading.Tasks;
using LNblitz.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using LNblitz.Data.Services;

namespace LNblitz.Pages.Wallets
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly WalletManager _walletManager;
        public Wallet Wallet { get; set; }

        public CreateModel(UserManager<User> userManager, WalletManager walletManager)
        {
            _userManager = userManager;
            _walletManager = walletManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var userId = _userManager.GetUserId(User);
            Wallet = new Wallet
            {
                UserId = userId,
                AdminKey = Guid.NewGuid().ToString(),
                InvoiceKey = Guid.NewGuid().ToString(),
                ReadonlyKey = Guid.NewGuid().ToString()
            };

            if (await TryUpdateModelAsync<Wallet>(Wallet, "wallet", w => w.Name))
            {
                await _walletManager.AddOrUpdateWallet(Wallet);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
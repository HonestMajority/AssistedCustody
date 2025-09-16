using AssistedCustody.Data;
using AssistedCustody.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NBitcoin;

namespace AssistedCustody.Pages;

public class ServerKeyModel : PageModel
{
    private readonly AssistedCustodyDbContext _dbContext;
    private readonly Network _network;

    public ServerKeyModel(AssistedCustodyDbContext dbContext, Network network)
    {
        _dbContext = dbContext;
        _network = network;
    }

    public string? Xprv { get; private set; }
    public string? StatusMessage { get; private set; }

    public async Task OnGetAsync()
    {
        Xprv = await _dbContext.ServerKeys
            .Select(serverKey => serverKey.XPriv)
            .FirstOrDefaultAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var newXprv = new ExtKey().GetWif(_network).ToString();

        var serverKey = await _dbContext.ServerKeys.FirstOrDefaultAsync();
        if (serverKey is null)
        {
            serverKey = new ServerKey
            {
                XPriv = newXprv,
            };
            _dbContext.ServerKeys.Add(serverKey);
            StatusMessage = "Generated and stored a new server key.";
        }
        else
        {
            serverKey.XPriv = newXprv;
            StatusMessage = "Replaced the existing server key with a new value.";
        }

        await _dbContext.SaveChangesAsync();

        Xprv = newXprv;

        return Page();
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using NBitcoin.RPC;

namespace AssistedCustody.Pages;

public class BitcoinInfoModel : PageModel
{
    public string? InfoJson { get; private set; }
    private readonly RPCClient _client;

    public BitcoinInfoModel(RPCClient client)
    {
        _client = client;
    }

    public async Task OnGet()
    {
        var response = await _client.SendCommandAsync(RPCOperations.getblockchaininfo);
        InfoJson = response.ResultString;
    }
}

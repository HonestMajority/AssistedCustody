using Microsoft.AspNetCore.Mvc.RazorPages;
using NBitcoin;
using NBitcoin.RPC;
using System.Net;

namespace AssistedCustody.Pages;

public class BitcoinInfoModel : PageModel
{
    public string? InfoJson { get; private set; }

    public async Task OnGet()
    {
        var rpcUser = Environment.GetEnvironmentVariable("BITCOIN_RPC_USER") ?? "user";
        var rpcPassword = Environment.GetEnvironmentVariable("BITCOIN_RPC_PASSWORD") ?? "password";
        var rpcUrl = Environment.GetEnvironmentVariable("BITCOIN_RPC_URL") ?? "http://bitcoind:18443";

        var credentials = new NetworkCredential(rpcUser, rpcPassword);
        var client = new RPCClient(credentials, new Uri(rpcUrl), Network.RegTest);
        var response = await client.SendCommandAsync(RPCOperations.getblockchaininfo);
        InfoJson = response.ResultString;
    }
}

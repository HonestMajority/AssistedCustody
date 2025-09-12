using Microsoft.AspNetCore.Mvc.RazorPages;
using NBitcoin;

namespace AssistedCustody.Pages;

public class MnemonicModel : PageModel
{
    private readonly Network _network;
    public string? Mnemonic { get; private set; }
    public string? Xprv { get; private set; }

    public MnemonicModel(Network network)
    {
        _network = network;
    }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        var m = new Mnemonic(Wordlist.English, WordCount.Twelve);
        Mnemonic = m.ToString();
        Xprv = m.DeriveExtKey().GetWif(_network).ToString();
    }
}

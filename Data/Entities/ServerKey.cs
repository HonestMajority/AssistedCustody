using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssistedCustody.Data.Entities;

public class ServerKey
{
    public int Id { get; set; }

    [Column("xpriv")]
    [Required]
    public string XPriv { get; set; } = string.Empty;
}

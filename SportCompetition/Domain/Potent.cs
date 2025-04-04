using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportCompetition.Model;

namespace SportCompetition.Domain;

public partial class Potent: BaseEntityDelExt
{

    public string lastname { get; set; } = null!;

    public string firstname { get; set; } = null!;

    public string? surname { get; set; }

    public DateOnly date_birth { get; set; }

    //public BitArray gender { get; set; } = null!;

    public string email { get; set; } = null!;

    public string login { get; set; } = null!;

    public string pass { get; set; } = null!;

    public DateTime dat_reg { get; set; }

    //public bool Deleted { get; set; }
}

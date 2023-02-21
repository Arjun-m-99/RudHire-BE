using System;
using System.Collections.Generic;

namespace Rudhire_BE.Models;

public partial class TblUserDetail
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public long PhoneNumber { get; set; }

    public string EmailId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Gender { get; set; }

    public DateTime? Dob { get; set; }

    public string? NickName { get; set; }

    public DateTime CreationDate { get; set; }

    public string? Role { get; set; }

    public virtual TblUserQualification? TblUserQualification { get; set; }
}

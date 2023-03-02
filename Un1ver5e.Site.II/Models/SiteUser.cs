using ArkLens.Models.Snapshots;
using Microsoft.AspNetCore.Identity;

namespace Un1ver5e.Site.II.Models;

public class SiteUser : IdentityUser
{
	public List<CharacterSnapshot> Characters { get; set; } = new();
}

using System;
using System.Collections.Generic;

namespace EmpoweredPixels.Utilities.Season
{
  public class SeasonConstants
  {
    public static readonly Guid Lynx = new Guid("4DE99EDF-3FED-40F7-B920-D2F7364EDBA4");

    public static readonly Guid Bull = new Guid("7C3EB882-3F6E-4D4C-9981-F18C4956A9F9");

    public static readonly Guid Frog = new Guid("DB9F6BFD-AF72-4DB9-9009-E0EE67455B65");

    public static readonly Guid Gorilla = new Guid("89E755C5-B53D-46C6-B3F1-C418D1DC32E4");

    public static readonly Guid Griffin = new Guid("3A98A10D-F2AE-4700-B65D-0A44625FC08D");

    public static readonly Guid Lion = new Guid("DF75D992-6B64-44D7-AAF3-FD07AEC1374B");

    public static readonly Guid Octopus = new Guid("92D0D0F9-57D5-4F49-B1FA-68E9AAD60846");

    public static readonly Guid Panda = new Guid("D087CE6B-7027-4CBA-94F3-5AF0299F5E68");

    public static readonly Guid Pegasus = new Guid("1E2C8876-FF2E-408E-9034-14667CBBA862");

    public static readonly Guid Mantis = new Guid("C34A9F16-8DE7-4240-AFA8-982BBC720117");

    public static readonly Guid Wyrm = new Guid("ECF7B8CA-7E58-47E1-8A6A-56B4694D4D27");

    public static readonly Guid Tiger = new Guid("A1C6EA93-5F75-4962-A86F-C26E2190A939");

    public static readonly Guid Wolf = new Guid("6B4D3986-1F25-4887-8C4A-AE44CDD034D1");

    public static readonly IReadOnlyList<Guid> Seasons = new Guid[]
    {
      Lynx,
      Bull,
      Frog,
      Gorilla,
      Griffin,
      Lion,
      Octopus,
      Panda,
      Pegasus,
      Mantis,
      Wyrm,
      Tiger,
      Wolf,
    };
  }
}

using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Match;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;

namespace EmpoweredPixels.Profiles.Match
{
  public class MatchResultProfile : Profile
  {
    public MatchResultProfile()
    {
      CreateMap<MatchResult, MatchDto>()
        .ForMember(o => o.Ticks, opt => opt.MapFrom(o => o.Ticks))
        .ForMember(o => o.Scores, opt => opt.MapFrom(o => o.Scores))
        .ForMember(o => o.TeamScores, opt => opt.MapFrom(o => o.TeamScores));

      CreateMap<EngineTick, TickDto>(MemberList.Destination);

      CreateMap<EngineRoundTick, RoundTickDto>()
        .ForMember(o => o.Ticks, opt => opt.MapFrom(o => o.Ticks))
        .ForMember(o => o.Scores, opt => opt.MapFrom(o => o.ScoreTick));

      CreateMap<IEngineRoundScoreTick, RoundScoreDto>(MemberList.Destination)
        .Include<EngineRoundScoreTick, RoundScoreDto>()
        .Include<EngineRoundTeamScoreTick, RoundScoreDto>()
        .ForMember(o => o.Id, opt => opt.Ignore());

      CreateMap<EngineRoundScoreTick, RoundScoreDto>(MemberList.Destination)
        .ForMember(o => o.Id, opt => opt.MapFrom(o => o.FighterId));

      CreateMap<EngineRoundTeamScoreTick, RoundScoreDto>(MemberList.Destination)
        .ForMember(o => o.Id, opt => opt.MapFrom(o => o.TeamId));

      CreateMap<FighterAttackTick, FighterAttackDto>(MemberList.Destination);

      CreateMap<FighterMoveTick, FighterMoveDto>(MemberList.Destination);

      CreateMap<FighterRegenerateHealthTick, FighterRegeneratedHealthDto>(MemberList.Destination);

      CreateMap<FighterRegenerateEnergyTick, FighterRegeneratedEnergyDto>(MemberList.Destination);

      CreateMap<EngineFighterDiedTick, FighterDiedTickDto>(MemberList.Destination)
        .ForMember(o => o.Died, opt => opt.Ignore());

      CreateMap<MatchScore, MatchScoreDto>(MemberList.Destination);
    }
  }
}

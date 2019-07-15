import { RoundTick } from './round-tick';
import { MatchScore } from './match-score';

export class MatchResult {
    public ticks: RoundTick[];

    public scores: MatchScore[];

    public teamScores: MatchScore[];
}

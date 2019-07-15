import { Tick } from './tick';
import { RoundScore } from './round-score';

export class RoundTick extends Tick {
    public round: number;

    public ticks: Tick[];

    public scores: RoundScore[];
}

import { Tick } from './tick';

export class FighterDiedTick extends Tick {
    public died: boolean;

    public fighterId: string;
}

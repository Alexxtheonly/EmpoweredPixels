import { Tick } from './tick';

export class FighterMove extends Tick {
    public fighterId: string;

    public currentX: number;

    public currentY: number;

    public currentZ: number;

    public nextX: number;

    public nextY: number;

    public nextZ: number;
}

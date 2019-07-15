import { Tick } from './tick';

export class RoundScore  extends Tick {
    public id: string;

    public damageDone: number;

    public damageTaken: number;

    public deaths: number;

    public distanceTraveled: number;

    public energy: number;

    public energyUsed: number;

    public health: number;

    public kills: number;

    public restoredEnergy: number;

    public restoredHealth: number;

    public round: number;
}

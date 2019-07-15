import { Tick } from './tick';

export class MatchScore extends Tick {
    public id: string;

    public maxHealth: number;

    public maxEnergy: number;

    public totalDamageDone: number;

    public totalDamageTaken: number;

    public totalEnergyUsed: number;

    public totalKills: number;

    public totalDeaths: number;

    public totalDistanceTraveled: number;

    public totalRegeneratedHealth: number;

    public totalRegeneratedEnergy: number;
}

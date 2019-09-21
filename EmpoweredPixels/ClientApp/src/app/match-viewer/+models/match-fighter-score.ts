import { Tick } from './tick';

export class MatchFighterScore extends Tick
{
    public fighterId: string;

    public fighterName: string;

    public username: string;

    public points: number;

    public teamId: string;

    public totalDamageDone: number;

    public totalDamageTaken: number;

    public totalEnergyUsed: number;

    public totalKills: number;

    public totalDeaths: number;

    public totalDistanceTraveled: number;

    public totalRegeneratedHealth: number;

    public totalRegeneratedEnergy: number;

    public roundsAlive: number;
}

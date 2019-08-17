import { GameFighterDamageinfo } from './game-fighter-damageinfo';
export class GameFighter
{
    public id: string;

    public name: string;

    public health: number;

    public currentHealth: number;

    public energy: number;

    public currentEnergy: number;

    public positionX: number;

    public positionY: number;

    public damageInfo: GameFighterDamageinfo;
}

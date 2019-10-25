import { SocketStone } from './socket-stone';
export class Equipment
{
    public id: string;

    public type: string;

    public userId: number;

    public fighterId: string;

    public isFavorite: boolean;

    public level: number;

    public rarity: number;

    public enhancement: number;

    public power: number;

    public conditionPower: number;

    public precision: number;

    public ferocity: number;

    public accuracy: number;

    public agility: number;

    public armor: number;

    public vitality: number;

    public healingPower: number;

    public speed: number;

    public vision: number;

    public isWeapon: boolean;

    public socketStones: SocketStone[];
}

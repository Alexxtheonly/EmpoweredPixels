import { Equipment } from './equipment';
export class Fighter
{
    public id: string;

    public name: string;

    public userId: number;

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

    public level: number;

    public equipment: Equipment[];
}

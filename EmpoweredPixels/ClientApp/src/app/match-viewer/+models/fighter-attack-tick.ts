import { Tick } from './tick';

export class FighterAttackTick extends Tick {
   public targetId: string;

   public fighterId: string;

   public skillId: string;

   public damage: number;

   public dodged: boolean;

   public critical: boolean;

   public outOfRange: boolean;

   public insufficientEnergy: boolean;
}

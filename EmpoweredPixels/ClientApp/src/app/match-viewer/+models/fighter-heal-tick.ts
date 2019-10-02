export class FighterHealTick
{
    public fighterId: string;

    public targetId: string;

    public healSkillId: string;

    public potentialHealing: number;

    public appliedHealing: number;

    public outOfRange: boolean;

    public onCooldown: boolean;
}

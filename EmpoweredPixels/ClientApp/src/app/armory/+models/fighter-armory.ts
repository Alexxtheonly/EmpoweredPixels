import { Fighter } from 'src/app/roster/+models/fighter';
export class FighterArmory
{
    public username: string;

    public offensiveRating: number;

    public defensiveRating: number;

    public eloRating: number;

    public eloRatingChange: number;

    public lastEloRatingUpdate: Date;

    public kills: number;

    public deaths: number;

    public killDeathRatio: number;

    public fighter: Fighter;

    public attunementId: string;
}

export class League
{
    public id: number;

    public name: string;

    public subscriberCount: number;

    public maxPowerlevel: number;

    public isTeam: boolean;

    public teamSize?: number;

    public nextMatch: Date;
}

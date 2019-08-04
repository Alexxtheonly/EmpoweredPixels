import { MatchOptions } from './match-options';
import { MatchRegistration } from './match-registration';
export class Match
{
    public id: string;

    public created: Date;

    public started: Date;

    public ended: boolean;

    public registrations: MatchRegistration[] = new Array();

    public options: MatchOptions;
}

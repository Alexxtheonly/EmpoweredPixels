import { MatchRegistration } from './match-registration';
export class Match {
    public id: string;

    public ended: boolean;

    public registrations: MatchRegistration[] = new Array();
}

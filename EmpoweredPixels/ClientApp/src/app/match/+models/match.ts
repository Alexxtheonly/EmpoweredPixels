import { MatchRegistration } from './match-registration';
export class Match {
    public id: string;

    public registrations: MatchRegistration[] = new Array();
}

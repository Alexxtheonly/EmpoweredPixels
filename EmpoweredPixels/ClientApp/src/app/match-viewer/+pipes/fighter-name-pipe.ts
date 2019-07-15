import { RosterService } from './../../roster/+services/roster.service';
import { PipeTransform, Pipe } from '@angular/core';

@Pipe({ name: 'fightername' })
export class FighterNamePipe implements PipeTransform {

    public constructor(private rosterService: RosterService) {

    }

    async transform(value: string) {
        const fightername = await this.rosterService.getFighterName(value).toPromise();

        if (fightername == null) {
            return '';
        }

        return fightername.name;
    }
}

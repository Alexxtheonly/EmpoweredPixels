import { RosterService } from './../../roster/+services/roster.service';
import { PipeTransform, Pipe } from '@angular/core';

@Pipe({ name: 'fightername' })
export class FighterNamePipe implements PipeTransform {

    public constructor(private rosterService: RosterService) {

    }

    transform(value: string) {

    }
}

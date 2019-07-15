import { Fighter } from './+models/fighter';
import { Component, OnInit } from '@angular/core';
import { RosterService } from './+services/roster.service';
import { observable, Observable } from 'rxjs';

@Component({
  selector: 'app-roster',
  templateUrl: './roster.component.html',
  styleUrls: ['./roster.component.css']
})
export class RosterComponent implements OnInit {
  private fighters: Fighter[] = new Array();

  constructor(private rosterService: RosterService) { }

  ngOnInit() {
    this.loadFighters();
  }

  private loadFighters() {
    this.rosterService.getFighters().subscribe(result => {
      this.fighters = result;
    }, error => {
      console.error(error);
    });
  }
}

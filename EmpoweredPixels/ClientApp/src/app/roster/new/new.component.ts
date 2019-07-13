import { Fighter } from './../+models/fighter';
import { Router } from '@angular/router';
import { RosterService } from './../+services/roster.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.css']
})
export class NewComponent implements OnInit {
  public name: string;
  public loading: boolean;

  constructor(private rosterService: RosterService, private router: Router) { }

  ngOnInit() {
  }

  public submit(): void {
    if (name || this.name.length === 0) {
      return;
    }

    this.loading = true;

    const fighter = new Fighter();
    fighter.name = this.name;

    this.rosterService.createFighter(fighter).subscribe(result => {
      this.router.navigate([`roster/fighter/${result.id}`]);
    }, error => console.error(error));
  }
}

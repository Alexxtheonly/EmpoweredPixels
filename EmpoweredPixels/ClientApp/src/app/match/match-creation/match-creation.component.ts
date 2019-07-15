import { Router } from '@angular/router';
import { MatchService } from './../+services/match.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-match-creation',
  templateUrl: './match-creation.component.html',
  styleUrls: ['./match-creation.component.css']
})
export class MatchCreationComponent implements OnInit {

  constructor(private matchService: MatchService, private router: Router) { }

  ngOnInit() {
    this.matchService.createMatch().subscribe(result => {
      this.router.navigate([`match/${result.id}`]);
    });
  }

}

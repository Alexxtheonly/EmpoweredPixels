import { MatchRegistration } from './../../+models/match-registration';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-match-participant',
  templateUrl: './match-participant.component.html',
  styleUrls: ['./match-participant.component.css']
})
export class MatchParticipantComponent implements OnInit {
  @Input()
  public registration: MatchRegistration;

  constructor() { }

  ngOnInit() {
  }

}

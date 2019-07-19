import { FooterService } from './+services/footer.service';
import { VersionInformation } from './+models/version-information';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  public versionInformation: VersionInformation;

  constructor(private footerService: FooterService) {
    this.footerService.getVersionInformation().subscribe(result => {
      this.versionInformation = result;
    });
  }

  ngOnInit() {
  }

}

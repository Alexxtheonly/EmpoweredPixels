import { FooterComponent } from './footer/footer.component';
import { AuthGuardService } from './auth/auth-guard.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { MatchViewerComponent } from './match-viewer/match-viewer.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AlertComponent } from './+directives/alert/alert/alert.component';
import { RosterComponent } from './roster/roster.component';
import { JwtInterceptor } from './+helpers/jwt-interceptor';
import { FighterComponent } from './roster/fighter/fighter.component';
import { NewFighterComponent } from './roster/new-fighter/new-fighter.component';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { FighterStatComponent } from './roster/fighter/+directives/fighter-stat/fighter-stat.component';
import { MatchCreationComponent } from './match/match-creation/match-creation.component';
import { MatchlobbyComponent } from './match/matchlobby/matchlobby.component';
import { MatchParticipantComponent } from './match/matchlobby/match-participant/match-participant.component';
import { TimeAgoPipe } from 'time-ago-pipe';
import { FighterNamePipe } from './match-viewer/+pipes/fighter-name-pipe';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { RoundsViewerComponent } from './match-viewer/+components/rounds-viewer/rounds-viewer.component';
import { MatchbrowserComponent } from './match/matchbrowser/matchbrowser.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgAggregatePipesModule, NgArrayPipesModule } from 'angular-pipes';
import { LeagueBrowserComponent } from './league/league-browser/league-browser.component';
import { LeagueDetailComponent } from './league/league-detail/league-detail.component';
import { GameFighterComponent } from './game-viewer/+components/game-fighter/game-fighter.component';
import { GameViewerComponent } from './game-viewer/game-viewer.component';

export function HttpLoaderFactory(http: HttpClient)
{
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    MatchViewerComponent,
    LoginComponent,
    RegisterComponent,
    AlertComponent,
    RosterComponent,
    FighterComponent,
    NewFighterComponent,
    FighterStatComponent,
    MatchCreationComponent,
    MatchlobbyComponent,
    MatchParticipantComponent,
    TimeAgoPipe,
    FighterNamePipe,
    FooterComponent,
    RoundsViewerComponent,
    MatchbrowserComponent,
    LeagueBrowserComponent,
    LeagueDetailComponent,
    GameViewerComponent,
    GameFighterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AngularFontAwesomeModule,
    NgxPaginationModule,
    NgAggregatePipesModule,
    NgArrayPipesModule,
    RouterModule.forRoot([
      { path: '', component: MatchbrowserComponent, pathMatch: 'full', canActivate: [AuthGuardService] },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'roster', component: RosterComponent, canActivate: [AuthGuardService] },
      { path: 'roster/fighter/new', component: NewFighterComponent, canActivate: [AuthGuardService] },
      { path: 'roster/fighter/:id', component: FighterComponent, canActivate: [AuthGuardService] },
      { path: 'match/create', component: MatchCreationComponent, canActivate: [AuthGuardService] },
      { path: 'match/:id', component: MatchlobbyComponent, canActivate: [AuthGuardService] },
      { path: 'match/:id/result', component: MatchViewerComponent, canActivate: [AuthGuardService] },
      { path: 'league', component: LeagueBrowserComponent, canActivate: [AuthGuardService] },
      { path: 'league/:id', component: LeagueDetailComponent, canActivate: [AuthGuardService] },
    ]),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

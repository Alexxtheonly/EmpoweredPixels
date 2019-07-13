import { AuthGuardService } from './auth/auth-guard.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { NewComponent } from './roster/new/new.component';

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
    NewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuardService] },
      { path: 'match', component: MatchViewerComponent, canActivate: [AuthGuardService] },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'roster', component: RosterComponent, canActivate: [AuthGuardService] },
      { path: 'roster/fighter/new', component: NewComponent, canActivate: [AuthGuardService] },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

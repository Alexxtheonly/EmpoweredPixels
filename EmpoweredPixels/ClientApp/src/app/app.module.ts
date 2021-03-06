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
import { RosterComponent } from './roster/roster.component';
import { JwtInterceptor } from './+helpers/jwt-interceptor';
import { FighterComponent } from './roster/fighter/fighter.component';
import { NewFighterComponent } from './roster/new-fighter/new-fighter.component';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { FighterStatComponent } from './roster/fighter/+directives/fighter-stat/fighter-stat.component';
import { MatchCreationComponent } from './match/match-creation/match-creation.component';
import { TimeAgoPipe } from 'time-ago-pipe';
import { FighterNamePipe } from './match-viewer/+pipes/fighter-name-pipe';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgAggregatePipesModule, NgArrayPipesModule } from 'angular-pipes';
import { LeagueBrowserComponent } from './league/league-browser/league-browser.component';
import { LeagueDetailComponent } from './league/league-detail/league-detail.component';
import { GameFighterComponent } from './game-viewer/+components/game-fighter/game-fighter.component';
import { GameViewerComponent } from './game-viewer/game-viewer.component';
import { MatchLogComponent } from './match-viewer/+components/match-log/match-log.component';
import { RewardComponent } from './rewards/+components/reward/reward.component';
import { RewardsComponent } from './rewards/rewards/rewards.component';
import { RewardContentComponent } from './rewards/+components/reward-content/reward-content.component';
import { ItemComponent } from './rewards/+components/item/item.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MomentModule } from 'ngx-moment';
import { VarDirective } from './+helpers/directives/var.directive';
import { LastLeagueWinnerPipe } from './league/+pipes/last-league-winner.pipe';
import { ArmoryComponent } from './armory/armory.component';
import { EquipmentComponent } from './+components/equipment/equipment.component';
import { FighterGearComponent } from './+components/fighter-gear/fighter-gear.component';
import { EnhancementComponent } from './inventory/enhancement/enhancement.component';
import { EquipmentInventoryComponent } from './inventory/equipment-inventory/equipment-inventory.component';
import { WebpackTranslateLoader } from './+helpers/webpack-translate-loader';
import { UserLeagueSubscriptionsPipe } from './league/+pipes/user-league-subscriptions.pipe';
import { ArmoryOverviewComponent } from './armory/armory-overview/armory-overview.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FighterExperienceComponent } from './roster/+components/fighter-experience/fighter-experience.component';
import { LeaguePanelComponent } from './dashboard/+components/league-panel/league-panel.component';
import { FighterPanelComponent } from './dashboard/+components/fighter-panel/fighter-panel.component';
import { FighterResultPanelComponent } from './dashboard/+components/fighter-result-panel/fighter-result-panel.component';
import { FighterAttunementsComponent } from './roster/+components/fighter-attunements/fighter-attunements.component';
import { FighterAttunementComponent } from './roster/+components/fighter-attunements/fighter-attunement/fighter-attunement.component';
import { ShopComponent } from './shop/shop.component';
import { CurrencyBalanceComponent } from './+components/currency-balance/currency-balance.component';
import { ShopItemComponent } from './shop/+components/shop-item/shop-item.component';
import { ShopItemPriceComponent } from './shop/+components/shop-item-price/shop-item-price.component';
import { SeasonPanelComponent } from './dashboard/+components/season-panel/season-panel.component';
import { SeasonComponent } from './season/season.component';

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
    RosterComponent,
    FighterComponent,
    NewFighterComponent,
    FighterStatComponent,
    MatchCreationComponent,
    TimeAgoPipe,
    FighterNamePipe,
    FooterComponent,
    LeagueBrowserComponent,
    LeagueDetailComponent,
    GameViewerComponent,
    GameFighterComponent,
    MatchLogComponent,
    RewardComponent,
    RewardsComponent,
    RewardContentComponent,
    ItemComponent,
    VarDirective,
    LastLeagueWinnerPipe,
    ArmoryComponent,
    EquipmentComponent,
    FighterGearComponent,
    EnhancementComponent,
    EquipmentInventoryComponent,
    UserLeagueSubscriptionsPipe,
    ArmoryOverviewComponent,
    DashboardComponent,
    FighterExperienceComponent,
    LeaguePanelComponent,
    FighterPanelComponent,
    FighterResultPanelComponent,
    FighterAttunementsComponent,
    FighterAttunementComponent,
    ShopComponent,
    CurrencyBalanceComponent,
    ShopItemComponent,
    ShopItemPriceComponent,
    SeasonPanelComponent,
    SeasonComponent,
  ],
  imports: [
    MomentModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AngularFontAwesomeModule,
    NgxPaginationModule,
    NgAggregatePipesModule,
    NgArrayPipesModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: DashboardComponent, canActivate: [AuthGuardService] },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'roster', component: RosterComponent, canActivate: [AuthGuardService] },
      { path: 'roster/fighter/new', component: NewFighterComponent, canActivate: [AuthGuardService] },
      { path: 'roster/fighter/:id', component: FighterComponent, canActivate: [AuthGuardService] },
      { path: 'match/:id/result', component: MatchViewerComponent, canActivate: [AuthGuardService] },
      { path: 'match/:id/log', component: MatchLogComponent, canActivate: [AuthGuardService] },
      { path: 'league', component: LeagueBrowserComponent, canActivate: [AuthGuardService] },
      { path: 'league/:id', component: LeagueDetailComponent, canActivate: [AuthGuardService] },
      { path: 'replay/:id', component: GameViewerComponent, canActivate: [AuthGuardService] },
      { path: 'rewards', component: RewardsComponent, canActivate: [AuthGuardService] },
      { path: 'leaderboard', component: ArmoryOverviewComponent, canActivate: [AuthGuardService] },
      { path: 'leaderboard/:id', component: ArmoryComponent, canActivate: [AuthGuardService] },
      { path: 'equipment/enhance/:id', component: EnhancementComponent, canActivate: [AuthGuardService] },
      { path: 'shop', component: ShopComponent, canActivate: [AuthGuardService] },
      { path: 'seasons', component: SeasonComponent, canActivate: [AuthGuardService] },
    ]),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useClass: WebpackTranslateLoader,
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

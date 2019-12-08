import { RewardContent } from './../rewards/+models/reward-content';
import { ShopFilter } from './+models/shop-filter';
import { ShopService } from './+services/shop.service';
import { Page } from './../match/+models/page';
import { InventoryService } from 'src/app/inventory/+services/inventory.service';
import { CurrencyBalance } from './../inventory/+models/currency-balance';
import { Component, OnInit } from '@angular/core';
import { ShopItem } from './+models/shop-item';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit
{

  public particlesBalance: CurrencyBalance;

  public commonTokenBalance: CurrencyBalance;

  public rareTokenBalance: CurrencyBalance;

  public fabledTokenBalance: CurrencyBalance;

  public mythicTokenBalance: CurrencyBalance;

  public balances: CurrencyBalance[];

  public filter: ShopFilter = new ShopFilter();

  public loading: boolean;

  public page: Page<ShopItem>;

  public content: RewardContent;

  public showModal: boolean;

  constructor(private inventoryService: InventoryService, private shopService: ShopService) { }

  async ngOnInit()
  {
    await this.updateBalances();
    this.filter.pageSize = 12;
    await this.loadShop();
  }

  public async updateParticlesBalance()
  {
    this.particlesBalance = await this.inventoryService.getParticleBalance().toPromise();
  }

  public async updateCommonTokenBalance()
  {
    this.commonTokenBalance = await this.inventoryService.getCommonTokenBalance().toPromise();
  }

  public async updateRareTokenBalance()
  {
    this.rareTokenBalance = await this.inventoryService.getRareTokenBalance().toPromise();
  }

  public async updateFabledTokenBalance()
  {
    this.fabledTokenBalance = await this.inventoryService.getFabledTokenBalance().toPromise();
  }

  public async updateMythicTokenBalance()
  {
    this.mythicTokenBalance = await this.inventoryService.getMythicTokenBalance().toPromise();
  }

  public async updateBalances()
  {
    await this.updateParticlesBalance();
    await this.updateCommonTokenBalance();
    await this.updateRareTokenBalance();
    await this.updateFabledTokenBalance();
    await this.updateMythicTokenBalance();

    this.balances = [this.particlesBalance,
    this.commonTokenBalance,
    this.rareTokenBalance,
    this.fabledTokenBalance,
    this.mythicTokenBalance];
  }

  public async loadPage(page: number)
  {
    this.loading = true;
    this.filter.pageNumber = page;
    await this.loadShop();
  }

  public async loadShop()
  {
    this.page = await this.shopService.getShopContent(this.filter).toPromise();
  }

  public async buyItem(item: ShopItem)
  {
    this.content = await this.shopService.buyItem(item).toPromise();
    this.showModal = true;
    await this.updateBalances();
  }
}

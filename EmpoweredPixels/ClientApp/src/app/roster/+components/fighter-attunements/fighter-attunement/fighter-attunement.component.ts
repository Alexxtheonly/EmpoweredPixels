import { Observable } from 'rxjs';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CurrencyBalance } from 'src/app/inventory/+models/currency-balance';
import { InventoryService } from 'src/app/inventory/+services/inventory.service';

@Component({
  selector: 'app-fighter-attunement',
  templateUrl: './fighter-attunement.component.html',
  styleUrls: ['./fighter-attunement.component.css']
})
export class FighterAttunementComponent implements OnInit
{

  @Input()
  public attunement: string;

  @Input()
  public strength: string;

  @Input()
  public weakness: string;

  @Input()
  public selected: string;

  public showAttunementConfirm: boolean;

  public costs = 5000;

  public balance: CurrencyBalance;

  @Output()
  public selectedChange = new EventEmitter<string>();

  constructor(private inventoryService: InventoryService)
  {
  }

  ngOnInit()
  {
  }

  public async updateBalance()
  {
    this.balance = await this.inventoryService.getBalance().toPromise();
  }

  public selectedChanged(id: string)
  {
    this.selectedChange.emit(id);
  }
}

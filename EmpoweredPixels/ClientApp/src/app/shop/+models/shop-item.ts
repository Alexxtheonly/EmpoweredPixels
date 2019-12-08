import { Item } from './../../rewards/+models/item';
import { ShopItemPrice } from './shop-item-price';
export class ShopItem extends Item
{
    public isEquipment: boolean;

    public categoryId: string;

    public prices: ShopItemPrice[];
}

export class Page<T>
{
    public items: T[] = new Array();
    public pageNumber = 1;
    public pageSize = 10;
    public total: number;
}

export class PagingOptions
{
    public pageNumber = 1;
    public pageSize = 10;
    public stringFilters: StringFilter[] = new Array();
    public numberFilters: NumberFilter[] = new Array();
    public sortSetting?: SortSetting;
}

export class StringFilter
{
    query?: string;
    propertyName?: string;
}

export class NumberFilter
{
    propertyName?: string;
    lowerbound?: number;
    upperbound?: number;
}

export class SortSetting
{
    propertyName?: string;
    descending?: boolean;
}

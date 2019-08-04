export class PagingOptions
{
    pageNumber = 1;
    pageSize = 10;
    stringFilters: StringFilter[] = new Array();
    numberFilters: NumberFilter[] = new Array();
    sortSetting?: SortSetting;
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

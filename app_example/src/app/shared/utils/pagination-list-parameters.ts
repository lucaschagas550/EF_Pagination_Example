import { HttpParams } from "@angular/common/http";

export class PaginationListParameters {
    page: number;
    size: number;
    search: string;
    sort: string;
    direction: string;
    customParams: Record<string, string>;

    constructor(
        page: number = 1,
        size: number = 10,
        sort: string = "",
        direction: string = "",
        search: string = "",
        customParams: Record<string, string> = {}
    ) {
        this.page = page;
        this.size = size;
        this.direction = direction;
        this.sort = sort;
        this.search = search;
        this.customParams = customParams;
    }

    getCustomParamsArrayKeyValue(): [string, string][] {
        return Object.entries(this.customParams);
    }

    getHttpParams(): HttpParams {
        let httpParams = new HttpParams()
            .append("page", this.page)
            .append("size", this.size)
            .append("sort", this.sort)
            .append("direction", this.direction)
            .append("search", this.search)

        return httpParams;
    }

    setSortNameToDefault(column: string): void {
        this.sort = column.charAt(0).toUpperCase() + column.slice(1);
    }
}
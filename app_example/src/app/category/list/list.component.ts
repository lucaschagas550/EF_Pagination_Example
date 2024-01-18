import { MatSort, Sort } from '@angular/material/sort';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { PaginationListParameters } from 'src/app/shared/utils/pagination-list-parameters';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  @ViewChild(MatSort) sort!: MatSort;
  displayedColumns: string[] = ['name', 'description'];
  categories !: Category[];
  paginationParams: PaginationListParameters = new PaginationListParameters();

  constructor(
    private categoryService: CategoryService,
    private _liveAnnouncer: LiveAnnouncer) {
  }

  ngOnInit(): void {
    this.GetCategories();
  }

  announceSortChange(sortState: Sort): void {
    if (sortState.direction) {
      this.paginationParams.direction = sortState.direction;
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this.paginationParams.direction = '';
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }

  sortByColumn(column: string): void {
    if (this.paginationParams.direction === '') {
      this.paginationParams.sort = '';
    } else {
      this.paginationParams.setSortNameToDefault(column);
    }

    this.GetCategories();
  }

  private GetCategories(): void {
    this.categoryService.getCategories(this.paginationParams).subscribe({
      next: (categories) => {
        //console.log("categorias result => ", categories);
        //testar sem a limpeza das categorias
        // this.categories = [];
        this.categories = categories;

      },
      error: (error) => { console.log("erro =>", error); }
    });
  }
}

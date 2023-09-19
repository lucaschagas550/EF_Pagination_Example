import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowValidationErrorsComponent } from './show-validation-errors.component';

describe('ShowValidationErrorsComponent', () => {
  let component: ShowValidationErrorsComponent;
  let fixture: ComponentFixture<ShowValidationErrorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowValidationErrorsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowValidationErrorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DivisionFilterComponent } from './division-filter.component';

describe('DivisionFilterComponent', () => {
  let component: DivisionFilterComponent;
  let fixture: ComponentFixture<DivisionFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DivisionFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DivisionFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

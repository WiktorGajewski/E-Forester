import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForestUnitFilterComponent } from './forest-unit-filter.component';

describe('ForestUnitFilterComponent', () => {
  let component: ForestUnitFilterComponent;
  let fixture: ComponentFixture<ForestUnitFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ForestUnitFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ForestUnitFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

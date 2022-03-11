import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForestUnitListComponent } from './forest-unit-list.component';

describe('ForestUnitListComponent', () => {
  let component: ForestUnitListComponent;
  let fixture: ComponentFixture<ForestUnitListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ForestUnitListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ForestUnitListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

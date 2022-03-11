import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignForestUnitComponent } from './assign-forest-unit.component';

describe('AssignForestUnitComponent', () => {
  let component: AssignForestUnitComponent;
  let fixture: ComponentFixture<AssignForestUnitComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AssignForestUnitComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AssignForestUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

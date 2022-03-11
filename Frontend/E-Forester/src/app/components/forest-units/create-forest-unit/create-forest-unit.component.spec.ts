import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateForestUnitComponent } from './create-forest-unit.component';

describe('CreateForestUnitComponent', () => {
  let component: CreateForestUnitComponent;
  let fixture: ComponentFixture<CreateForestUnitComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateForestUnitComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateForestUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

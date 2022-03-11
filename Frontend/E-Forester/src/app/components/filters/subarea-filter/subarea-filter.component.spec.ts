import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubareaFilterComponent } from './subarea-filter.component';

describe('SubareaFilterComponent', () => {
  let component: SubareaFilterComponent;
  let fixture: ComponentFixture<SubareaFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubareaFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubareaFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

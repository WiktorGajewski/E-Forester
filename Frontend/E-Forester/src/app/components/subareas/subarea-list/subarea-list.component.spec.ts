import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubareaListComponent } from './subarea-list.component';

describe('SubareaListComponent', () => {
  let component: SubareaListComponent;
  let fixture: ComponentFixture<SubareaListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubareaListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubareaListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

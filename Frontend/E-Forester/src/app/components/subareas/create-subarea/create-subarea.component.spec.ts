import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSubareaComponent } from './create-subarea.component';

describe('CreateSubareaComponent', () => {
  let component: CreateSubareaComponent;
  let fixture: ComponentFixture<CreateSubareaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateSubareaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateSubareaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

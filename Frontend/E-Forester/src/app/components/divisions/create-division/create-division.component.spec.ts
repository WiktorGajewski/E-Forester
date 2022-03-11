import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateDivisionComponent } from './create-division.component';

describe('CreateDivisionComponent', () => {
  let component: CreateDivisionComponent;
  let fixture: ComponentFixture<CreateDivisionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateDivisionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateDivisionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

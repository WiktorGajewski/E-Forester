import { TestBed } from '@angular/core/testing';

import { PlanItemService } from './plan-item.service';

describe('PlanItemService', () => {
  let service: PlanItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlanItemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

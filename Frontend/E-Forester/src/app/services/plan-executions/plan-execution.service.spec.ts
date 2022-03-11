import { TestBed } from '@angular/core/testing';

import { PlanExecutionService } from './plan-execution.service';

describe('PlanExecutionService', () => {
  let service: PlanExecutionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlanExecutionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

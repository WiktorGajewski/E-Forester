import { TestBed } from '@angular/core/testing';

import { ForestUnitService } from './forest-unit.service';

describe('ForestUnitService', () => {
  let service: ForestUnitService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ForestUnitService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

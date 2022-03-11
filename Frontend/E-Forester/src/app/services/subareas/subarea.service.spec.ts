import { TestBed } from '@angular/core/testing';

import { SubareaService } from './subarea.service';

describe('SubareaService', () => {
  let service: SubareaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubareaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

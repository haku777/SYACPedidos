import { TestBed } from '@angular/core/testing';

import { ServiceClientes } from './service-clientes';

describe('ServiceClientes', () => {
  let service: ServiceClientes;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceClientes);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

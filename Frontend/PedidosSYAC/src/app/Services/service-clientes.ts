import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ServiceClientes {
  

  private apiUrl = 'https://localhost:7127/api/Clientes/GetClientes';

    constructor(private http: HttpClient) {}

    obtenerClientes(): Observable<any[]> {
      return this.http.get<any[]>(this.apiUrl);
    }


}

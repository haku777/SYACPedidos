import { Component } from '@angular/core';
import { ServiceClientes } from '../Services/service-clientes';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-clientes',
  imports: [CommonModule],
  templateUrl: './clientes.html',
  styleUrl: './clientes.css',
})
export class Clientes {
  listaClientes= [{id: 1, nombre: 'cliente 1'}, {id: 2, nombre: 'cliente 2'}];

  
  constructor (private serviceClientes: ServiceClientes) {}
  
  ngOnInit() {
    this.serviceClientes.GetClientes().subscribe(data => {this.listaClientes = data;});

    // this.serviceClientes.GetClientes().subscribe(
    //   (data) => {
    //     this.listaClientes = data;
    //     console.log('Clientes obtenidos:', this.listaClientes);
    //   },
    //   (error) => {
    //     console.error('Error al obtener clientes:', error);
    //   }
    // );

  }







}

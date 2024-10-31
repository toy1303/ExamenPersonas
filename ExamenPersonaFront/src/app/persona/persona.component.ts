import { Component, OnInit } from '@angular/core';
import {PrincipalService} from '../servicio/principal.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2'
import 'sweetalert2/src/sweetalert2.scss'

@Component({
  selector: 'app-persona',
  standalone: true,
  imports: [CommonModule,FormsModule ],
  templateUrl: './persona.component.html',
  styleUrl: './persona.component.css'
})
export class PersonaComponent implements OnInit {

  titulomodal:string="";
  MNombre:string="";
  MEdad:Number=0;
  MCorreo:string="";
  Mid:number=0;
  Personas:any;
  Mopt:Number=0;
  constructor(private Servicio:PrincipalService,
    private ServicioModal: NgbModal 
  )
  {

  }
  ngOnInit(): void {
    this.ObtienePersonas();
  }


  async ObtienePersonas()
  {
    this.Personas= await this.Servicio.ObtienePersonas();
  }




  async AbreModal(opt:Number, detallle:any, p:any)
  {
    this.Mopt=opt;
    if(this.Mopt==1) //Modifica El Modal
    {
      this.MNombre=p.nombre;
      this.MEdad=p.edad;
      this.MCorreo=p.email;
      this.Mid=p.id;
      this.titulomodal="Modifica Persona";
    }
    else
    {
      this.MNombre="";
      this.MEdad=0;
      this.MCorreo="";
      this.Mid=0;
      this.titulomodal="Agrega Persona";
    }
    this.ServicioModal.open(detallle, { size: 'lg' });
  }

  async AgregaPersona()
  {
    let Persona={
      Nombre:this.MNombre,
      Edad:this.MEdad,
      Email: this.MCorreo
    }
    let respuesta = await this.Servicio.AgregaPersonas(Persona);
    this.ServicioModal.dismissAll();
    if(respuesta.mensaje=="Operación Exitosa")
    {
      Swal.fire({
        title: respuesta.mensaje,
        icon: 'success',
        customClass: {
          icon: 'rotate-y',
        },
      })
    }
    else
    {
      Swal.fire({
        title: respuesta.mensaje,
        icon: 'error',
        customClass: {
          icon: 'rotate-y',
        },
      })
    }

    this.ObtienePersonas();
  }

  async ModificaPersona()
  {
    let Persona={
      Id: this.Mid,
      Nombre:this.MNombre,
      Edad:this.MEdad,
      Email: this.MCorreo
    }
    let respuesta = await this.Servicio.ModificaPersonas(Persona);
    this.ServicioModal.dismissAll();
    if(respuesta.mensaje=="Operación Exitosa")
    {
      Swal.fire({
        title: respuesta.mensaje,
        icon: 'success',
        customClass: {
          icon: 'rotate-y',
        },
      })
    }
    else
    {
      Swal.fire({
        title: respuesta.mensaje,
        icon: 'error',
        customClass: {
          icon: 'rotate-y',
        },
      })
    }
    this.ObtienePersonas();
  }

  async EliminaPersona(Id:number)
  {
    let respuesta = await this.Servicio.EliminaPersonas(Id);
    this.ServicioModal.dismissAll();
    if(respuesta.mensaje=="Operación Exitosa")
    {
      Swal.fire({
        title: respuesta.mensaje,
        icon: 'success',
        customClass: {
          icon: 'rotate-y',
        },
      })
    }
    else
    {
      Swal.fire({
        title: respuesta.mensaje,
        icon: 'error',
        customClass: {
          icon: 'rotate-y',
        },
      })
    }
    this.ObtienePersonas();
  }
}

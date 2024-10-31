import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PrincipalService {

  private apiUrl = 'https://localhost:7181/'; 
  constructor(private http:HttpClient) { }


  async ObtienePersonas(): Promise<any> {
    const response = await this.http.get<any>(this.apiUrl+'ObtienePersonas').toPromise();
    // Extraer solo el array de datos
    return response;
  }

  async AgregaPersonas(Persona:any): Promise<any> {
    const response = await this.http.post<any>(this.apiUrl+'agregapersona',Persona).toPromise();
    // Extraer solo el array de datos
    return response;
  }

  async ModificaPersonas(Persona:any): Promise<any> {
    const response = await this.http.put<any>(this.apiUrl+'modificapersona',Persona).toPromise();
    // Extraer solo el array de datos
    return response;
  }
  async EliminaPersonas(Id:number): Promise<any> {
    const response = await this.http.delete<any>(this.apiUrl+'eliminapersona/'+Id).toPromise();
    // Extraer solo el array de datos
    return response;
  }

}

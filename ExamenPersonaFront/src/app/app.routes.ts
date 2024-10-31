import { NgModule } from '@angular/core';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';
import { PersonaComponent } from './persona/persona.component';
export const routes: Routes = [
    {
        path:'',
        component:PersonaComponent
    }
];


@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule]
  })

  export class AppRoutingModule { }
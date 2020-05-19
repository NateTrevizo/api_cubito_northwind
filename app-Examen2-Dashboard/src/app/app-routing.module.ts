import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
import { AcercaDeComponent } from './pages/acerca-de/acerca-de.component';
import { Top5Component } from './pages/top5/top5.component';
import { HistogramaComponent } from './pages/histograma/histograma.component';


const routes: Routes = [
  {path:'inicio',component:InicioComponent},
  {path:'acerca-de',component: AcercaDeComponent},
  {path:'top5',component: Top5Component},
  {path:'histograma',component: HistogramaComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{useHash:true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }

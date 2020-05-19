import { BrowserModule } from '@angular/platform-browser';
import { NgModule,NO_ERRORS_SCHEMA,CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ChartsModule } from 'ng2-charts';
import {HttpClientModule} from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import {NgSelectModule} from '@ng-select/ng-select';
import { AppComponent } from './app.component';
import {FormsModule} from '@angular/forms';
import { InicioComponent } from './pages/inicio/inicio.component';
import { AcercaDeComponent } from './pages/acerca-de/acerca-de.component';
import { Top5Component } from './pages/top5/top5.component';
import { HistogramaComponent } from './pages/histograma/histograma.component';
import { NavbarComponent } from './components/navigation/navbar/navbar.component';
import { LineChartComponent } from './components/charts/line-chart/line-chart.component';
import { BarChartComponent } from './components/charts/bar-chart/bar-chart.component';
import { PieChartComponent } from './components/charts/pie-chart/pie-chart.component';

@NgModule({
  declarations: [
    AppComponent,
    InicioComponent,
    AcercaDeComponent,
    Top5Component,
    HistogramaComponent,
    NavbarComponent,
    LineChartComponent,
    BarChartComponent,
    PieChartComponent
  ],
  imports: [
    BrowserModule,
    NgSelectModule,
    FormsModule,
    AppRoutingModule,
    ChartsModule,
    HttpClientModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA,
    NO_ERRORS_SCHEMA],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

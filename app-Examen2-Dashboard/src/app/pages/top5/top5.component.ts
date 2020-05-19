import { Component, OnInit, Input } from '@angular/core';
import { NorthwindService } from 'src/app/services/northwind.service';
import { Label } from 'ng2-charts';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-top5',
  templateUrl: './top5.component.html',
  styleUrls: ['./top5.component.scss']
})
export class Top5Component implements OnInit {

  constructor(private north: NorthwindService) { }
  //Data variables
  @Input() selectDimension:string="";
  dataTop5:any;
  dataDimension:Label[]=[];
  dataValues:number[]=[];
  
 
//DATA NG SELECT
  defaultBindingsList = [
    { value: 1, label: 'Cliente' },
    { value: 2, label: 'Producto' },
    { value: 3, label: 'Empleado'}
];

//Ng-select multiple item dimension
cliente$: Observable<any>;
selectedItemDim:any[]=[];

//Ng-select multiple año
anio$: Observable<any>;
selectedAnio:any[]=[];

//Ng-select multiple mes
mes$: Observable<any>;
selectedMes:any[]=[];

valoresBody:string="";
//valoresBody:any[]=[];
selectedDimension = null;
selectedItemsDimension=[];
  ngOnInit(): void {
    this.mes$=this.north.getMonths();
    this.anio$=this.north.getYears();
    this.selectedDimension = this.defaultBindingsList[0];
    this.selectDimension="Selecciona "+this.selectedDimension.label;
    this.cliente$ = this.north.getItemsByDimension(this.selectedDimension.label,'DESC');
  }

  onChangedDimension($event){
    console.log('Item seleccionado: ', $event);
    this.selectedItemDim = [];
    this.selectedAnio = [];
    this.selectedMes=[];
    this.selectedDimension=$event;
    this.selectDimension="Selecciona "+this.selectedDimension.label;
   
    this.cliente$ = this.north.getItemsByDimension(this.selectedDimension.label,'DESC');
  }

  clearModel() {
    this.selectedItemDim = [];
  }

changeModel() {
    this.selectedItemDim = [{ name: 'Nuevo cliente' }];
}

valorJson = [];
onChangedItemDimension($event){
  this.valorJson=[
    {itemdim:this.selectedItemDim ,anios:this.selectedAnio,meses:this.selectedMes},
  ]
  //this.valoresBody="{ itemdim:"+this.selectedItemDim+", anios:"+this.selectedAnio+", meses:"+this.selectedMes+"}";
    this.north.getDataPieByDimension(this.selectedDimension.label,'DESC',this.valorJson[0]).subscribe((result:any)=>{
      this.dataDimension=result.datosDimension;
      this.dataValues=result.datosVenta;
    });
}


onChangedYear($event){
  this.valorJson=[
    {itemdim:this.selectedItemDim ,anios:this.selectedAnio,meses:this.selectedMes},
  ]
  //this.valoresBody="{ itemdim:"+this.selectedItemDim+", anios:"+this.selectedAnio+", meses:"+this.selectedMes+"}";
    this.north.getDataPieByDimension(this.selectedDimension.label,'DESC',this.valorJson[0]).subscribe((result:any)=>{
      this.dataDimension=result.datosDimension;
      this.dataValues=result.datosVenta;
    });
}




}

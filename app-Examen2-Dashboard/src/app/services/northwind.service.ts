import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

const URL_API=environment.API.EndPoints.Northwind;

@Injectable({
  providedIn: 'root'
})
export class NorthwindService {

  constructor(private http:HttpClient) {

  }

  headers={
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
}
  getDataPieByDimension(dimension:string, orden:string, values:string){
    return this.http.post(`${URL_API}GetDataPieByDimension/${dimension}/${orden}`,values,this.headers).pipe(
      map((result:any)=> result)
    );
  }


  getTop5(dimension:string, orden:string){
    return this.http.get(`${URL_API}Top5/${dimension}/${orden}`);
  }

  getMonths(){
    return this.http.get(`./assets/json/meses.json`).pipe(
      map((result:any)=> result.meses)
    )
  }

  getItemsByDimension(dimension:string,orden:string){
    return this.http.get(`${URL_API}GetItemsByDimension/${dimension}/${orden}`).pipe(
      map((result:any)=> result.datosDimension)
    )
  }

  getYears(){
    return this.http.get(`${URL_API}GetYears`).pipe(
      map((result:any)=> result.datosDimension)
    )
  }

  getDataBarByDimension(dimension:string, orden:string, values:string){
    return this.http.post(`${URL_API}GetDataBarByDimension/${dimension}/${orden}`,values,this.headers).pipe(
      map((result:any)=> result)
    );
  }

  
}

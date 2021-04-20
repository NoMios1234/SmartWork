import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly APIUrl = "https://localhost:44334/api";
  readonly PhotoUrl = "https://localhost:44334/Photos";

  constructor(private http:HttpClient) { }

  getCompanyList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/Companies');
  }
  getCompany(id:any){
    return this.http.get(this.APIUrl+'/Companies/', id);
  }
  getCompanyOffices(id:any){
    return this.http.get(this.APIUrl+'/Companies/GetCompanyOffices/', id);
  }
  addCompany(company:any){
    return this.http.post(this.APIUrl+'/Companies', company);
  }
  updateCompany(company:any){
    return this.http.put(this.APIUrl+'/Companies', company);
  }
  deleteCompany(val:any){
    return this.http.delete(this.APIUrl+'/Companies/', val);
  }
  uploadCompanyPhoto(val:any){
    return this.http.post(this.PhotoUrl+'/Companies/SaveFile', val);
  }
}

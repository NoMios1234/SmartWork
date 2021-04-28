import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly APIUrl = "https://localhost:44334/api";
  readonly PhotoUrl = "https://localhost:44334/wwwroot/Photos";
 

  constructor(private http:HttpClient) { }

  getCompanyList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/Companies');
  }
  addCompany(company:any){
    return this.http.post(this.APIUrl+'/Companies', company);
  }
  updateCompany(company:any){
    return this.http.put(this.APIUrl+'/Companies', company);
  }
  deleteCompany(id:any){
    console.log('delete id:' + id);
    return this.http.delete(this.APIUrl+'/Companies/' + id);
  }
  addCompanyPhoto(val:any){
    return this.http.post(this.APIUrl+'/Companies/SaveFile', val)
  }
  getOfficeList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/Offices');
  }
  addOffice(office:any){
    return this.http.post(this.APIUrl+'/Offices', office);
  }
  updateOffice(office:any){
    return this.http.put(this.APIUrl+'/Offices', office);
  }
  deleteOffice(id:any){
    return this.http.delete(this.APIUrl+'/Offices/' + id);
  }
  addOfficePhoto(val:any){
    return this.http.post(this.APIUrl+'/Offices/SaveFile', val)
  }
  getOfficeRooms(id:any){
    return this.http.get<any>(this.APIUrl+'/Offices/GetOfficeRooms', id);
  }
}

import { Injectable } from '@angular/core';

// import the http client and observables module
import { HttpClient } from '@angular/common/http';

// OBSERVABLES ARE BASICALLY USED TO HANDLE HANLDE ASYNCHRONOUS REQUEST AND RESPONSES
import { Observable,throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class SharedService {
  // add the necessary urls for the project like the api url (RUN DAT BACKEND/DB PROJECT ON VS 2019 YA BASTARD!) and photo url
  readonly APIurl = 'http://localhost:5000/api';
  readonly PhotoUrl = 'http://localhost:5000/Photos';

  // instantiate the http client and constructor
  constructor(private http: HttpClient) { }

  // ------------------------------- DEPARTMENT-------------------------------
  // add a method to consume the DEPARTMENT data API
  getDepList(): Observable<any[]> {

    return this.http.get<any>(this.APIurl + '/department');
  }

  // add a method to consume the Post DEPARTMENT data API to add a new department

  addDepartment(val: any) {
    return this.http.post(this.APIurl + '/department', val);
  }
  // update DEPARTMENT details API

  updateDepartment(val: any) {
    return this.http.put(this.APIurl + '/department', val).pipe(catchError(this.errorHandler));
  }

  // delete DEPARTMENT API
  deleteDepartment(val: any){
    return this.http.delete(this.APIurl + '/department/' + val);
  }


  // ------------------------ EMPLOPYEE--------------------
  // add a method to consume the EMPLOYEE data API
  getEmpList(): Observable<any[]> {
    return this.http.get<any>(this.APIurl + '/employee');
  }

  // add a method to consume the Post EMPLOYEE data API to add a new department

  addEmployee(val: any) {
    return this.http.post(this.APIurl + '/employee', val);
  }
  // update EMPLOYEE details API

  updateEmployee(val: any) {
    return this.http.put(this.APIurl + '/employee', val);
  }

  // delete EMPLOYEE API
  deleteEmployee(val: any){
    return this.http.delete(this.APIurl + '/employee/' + val);
  }

  // method to save profile pictures

  UploadPhoto(val: any){
    return this.http.post(this.APIurl + '/Employee/SaveFile' , val);
  }
// method to display all department names that is associated with employees
  getAllDepartmentNames(): Observable<any[]> {

    return this.http.get<any>(this.APIurl + '/Employee/GetAllDepartmentNames');
  }


  errorHandler(error){
    let errorMessage = '';
    if(error.error instanceof ErrorEvent){
      errorMessage = error.error.message;
    }else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
  }

}

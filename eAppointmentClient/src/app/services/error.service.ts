import { Injectable } from '@angular/core';
import { SwalService } from './swal.service';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(
    private swal: SwalService
  ) { }

  errorHandler(err:HttpErrorResponse){
console.log(err)
let message="Error!"
if(err.status===0){
  message= "Api is not available";
} else if(err.status===404){
  message="Api is not found";
}else if(err.status===401){
  message="you are not authorized"
}

this.swal.callToast(message,"error");

  }
}

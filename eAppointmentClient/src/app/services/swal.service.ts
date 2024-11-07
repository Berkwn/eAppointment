import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class SwalService {

  constructor() { }

  callToast(title:string,icon:SweetAlertIcon="success"){
    Swal.fire({
      title:title,
      timer:3000,
      icon:icon,
      showCloseButton:false,
      showCancelButton:false,
      position:"bottom-right",
      showConfirmButton:false,
      toast:true
    })
  }


  callSwal(title:string,text:string,callback: ()=>void){
    Swal.fire({
      title:title,
      text:text,
      icon:"question",
      showCloseButton:true,
      cancelButtonText:"Cancel",
      showCancelButton:true,
      showConfirmButton:true,
      confirmButtonText:"DELETE"
    }).then(res=>{
      if(res.isConfirmed){
        callback();
      }
    })
  }
}
  export type SweetAlertIcon = 'success' | 'error' | 'warning' | 'info' | 'question'
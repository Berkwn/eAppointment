import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PatientModel } from '../../../models/patient.model';
import { HttpService } from '../../../services/http.service';
import { SwalService } from '../../../services/swal.service';
import { PatientPipe } from '../../../pipe/patient.pipe';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-patient',
  standalone: true,
  imports: [CommonModule,PatientPipe,FormsModule,FormValidateDirective,RouterLink],
  templateUrl: './patient.component.html',
  styleUrl: './patient.component.css'
})
export class PatientComponent implements OnInit {
  patients:PatientModel[]=[];
  createPatientModel:PatientModel=new PatientModel();
  updatePatientModel:PatientModel=new PatientModel();

  @ViewChild("addModalCloseButton") addModalCloseButton:ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("UpdateModalCloseButton")  UpdateModalCloseButton:ElementRef<HTMLButtonElement> | undefined

  ngOnInit(): void {
    this.getAll();
  }

search:string=""


constructor(

  private http:HttpService,
  private swal:SwalService
) {

  
}
 

getAll(){
  this.http.post<PatientModel[]>("Patients/GetAll",{},(res)=>{
    this.patients=res.data;
    
  })
}


add(form:NgForm){
  if(form.valid){
    this.http.post<string>("Patients/Create",this.createPatientModel,(res)=>{
      this.swal.callToast(res.data,"success")

      this.getAll();
      this.addModalCloseButton?.nativeElement.click();
      this.createPatientModel=new PatientModel();
      
    })



  }

}

delete(id:string,fullName:string){
this.swal.callSwal("Delete",`You want to delete ${fullName}`,()=>{
this.http.post<string>("Patients/Delete",{id:id},(res)=>{
  this.swal.callToast(res.data,"success")
  this.getAll();
})

})

}

get(data:PatientModel){
this.updatePatientModel={...data}

}


update(form:NgForm){
if(form.valid){
  this.http.post<string>("Patients/Update",this.updatePatientModel,(res)=>{
    console.log(res)
    this.swal.callToast(res.data,"success")
    this.getAll();
    this.UpdateModalCloseButton?.nativeElement.click();

  })
}

}


}




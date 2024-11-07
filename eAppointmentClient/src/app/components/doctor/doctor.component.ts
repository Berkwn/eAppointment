import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpService } from '../../services/http.service';
import { doctorModel } from '../../models/doctor.model';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';
import { SwalService } from '../../services/swal.service';
import { DoctorPipe } from '../../pipe/doctor.pipe';
import { departments } from '../../models/constants';

@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    FormValidateDirective,
    DoctorPipe,
  ],
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent implements OnInit {
  
  @ViewChild("addModalCloseButton") addModalCloseButton: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  doctors: doctorModel[] = [];
  departments=departments;

  createModel: doctorModel = new doctorModel();
  updateModel: doctorModel = new doctorModel();
  search: string = "";

  constructor(
    private http: HttpService,
    private swal: SwalService
  ) { }

  ngOnInit(): void {
    this.GetAll();
  }

  GetAll() {
    this.http.post<doctorModel[]>("Doctors/GetAll", {}, (res) => {
      this.doctors = res.data;
    });
  }

  add(form: NgForm) {
    if (form.valid) {
      this.http.post<string>("Doctors/Create", this.createModel, (res) => {
        this.swal.callToast(res.data, "success");
        this.GetAll();
        this.addModalCloseButton?.nativeElement.click();
        this.createModel = new doctorModel();
      });
    }
  }

  delete(id: string, fullName: string){
    this.swal.callSwal("Delete doctor?",`You want to delete ${fullName}?`,()=> {
      this.http.post<string>("Doctors/DeleteById", {id: id}, (res)=> {
        this.swal.callToast(res.data,"info");
        this.GetAll();
      })
    })
  }

  get(data: doctorModel){    
    this.updateModel = {...data};
    this.updateModel.departmentvalue = data.department.value;
  }

  update(form:NgForm){
    if(form.valid){
      this.http.post<string>("Doctors/Update",this.updateModel,(res)=> {
        this.swal.callToast(res.data,"success");
        this.GetAll();
        this.updateModalCloseBtn?.nativeElement.click();        
      });
    }
  }

}

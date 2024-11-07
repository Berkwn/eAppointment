import { Component, ElementRef, ViewChild } from '@angular/core';
import { departments } from '../../models/constants';
import { doctorModel } from '../../models/doctor.model';
import { DoctorComponent } from '../doctor/doctor.component';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { DxSchedulerModule } from 'devextreme-angular';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { AppointmentModel } from '../../models/Appointment.model';
import { CreateAppointmentModel } from '../../models/createAppointment.model';
import { DatePickerType } from 'devextreme/ui/date_box';
import { PatientModel } from '../../models/patient.model';

declare const $:any;
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, CommonModule, DxSchedulerModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  providers: [DatePipe]
})
export class HomeComponent {

  @ViewChild("addModalCloseButton") addModalCloseButton : ElementRef<HTMLButtonElement> | undefined

  createModel:CreateAppointmentModel= new CreateAppointmentModel();
  departments = departments;
  doctors: doctorModel[] = [];

  selectedDepartmentvalue: number = 0;
  selectedDoctorId: string = '';

  appointments: AppointmentModel[] = [];

  constructor(
    private http: HttpService,
    private date:DatePipe,
    private swal:SwalService
  
  ) {}

  getAllDoctor() {
    this.selectedDoctorId = '';
    if (this.selectedDepartmentvalue > 0) {
      this.http.post<doctorModel[]>(
        'Appointments/GetDoctorsByDepartment',
        { departmentValue: +this.selectedDepartmentvalue },
        (res) => {
          this.doctors = res.data;
        }
      );
    }
  }

  getAllAppointments() {
    if (this.selectedDoctorId) {
      this.http.post<AppointmentModel[]>(
        'Appointments/GetAllByDoctorId',
        { doctorId: this.selectedDoctorId },
        (res) => {
          this.appointments = res.data;
        }
      );
    }
  }

  onAppointmentFormOpening(event:any){
    console.log(event)
    event.cancel=true;
    this.createModel.startDate=this.date.transform(event.appointmentData.startDate,"dd,MM.yyyy HH:mm") ?? "";
    this.createModel.endDate=this.date.transform(event.appointmentData.endDate,"dd,MM.yyyy HH:mm") ?? "";
    this.createModel.doctorId=this.selectedDoctorId;

    $("#addModal").modal("show");
  }



  create(form:NgForm){
    if(form.valid){
      this.http.post<CreateAppointmentModel>("Appointments/Create",this.createModel,(res)=>{
        this.swal.callToast(res.data);
        this.addModalCloseButton?.nativeElement.click();
        this.createModel=new CreateAppointmentModel();
        this.getAllAppointments();
      })
    }
  }

  getPatient(){
this.http.post<PatientModel>("Appointments/GetPatientByIdentityNumber",{identityNumber:this.createModel.identityNumber},(res)=>{

  console.log(res.data)
  if(res.data==null){
    this.createModel.firstName=""
    this.createModel.lastName=""
    this.createModel.city=""
    this.createModel.town=""
    this.createModel.fullAddress=""
    this.createModel.patientId=null;

    return;
  }

else{
  this.createModel.patientId=res.data.id;
  this.createModel.firstName=res.data.firstName;
  this.createModel.lastName=res.data.lastName;
  this.createModel.city=res.data.city;
  this.createModel.town=res.data.town;
  this.createModel.fullAddress=res.data.fullAddress;
  console.log(this.createModel.city)
  console.log(this.createModel.town)
}
})
  }


 
  onAppointmentDeleted(event:any){
    event.cancel=true
  
  }  

  onAppointmentDeleting(event:any){
    event.cancel=true
    console.log(event)
    this.swal.callSwal("Delete Appointment",`Are you want to delete ${event.appointmentData.patient.fullName}`,()=>{
      this.http.post<string>("Appointments/DeleteById",{id:event.appointmentData.id},(res)=>{
        this.swal.callToast(res.data,"info")
        this.getAllAppointments();

      })
    })
      
   
  }

  onAppointmentUpdating(event:any){
    event.cancel=true
console.log(event)
    const data={
      id:event.newData.id,
      startDate:this.date.transform(event.newData.startDate,"MM.dd.yyyy HH:mm"),
      endDate:this.date.transform(event.newData.endDate,"MM.dd.yyyy HH:mm")

    }
    console.log(data)

    this.http.post<string>("Appointments/Update",data,(res)=>{
      this.swal.callToast(res.data,"success")
      this.getAllAppointments();
    })
  }
}

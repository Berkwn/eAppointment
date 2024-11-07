import { PatientModel } from "./patient.model";

export class AppointmentModel{
    Id:string="";
    StartDate:string="";
    EndDate:string="";
    Title:string="";
    Patient:PatientModel= new PatientModel();
}
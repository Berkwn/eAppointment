import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { PatientPipe } from '../../pipe/patient.pipe';
import { FormsModule, NgForm } from '@angular/forms';
import { UserPipe } from '../../pipe/user.pipe';
import { FormValidateDirective } from 'form-validate-angular';
import { RouterLink } from '@angular/router';
import { UserModel } from '../../models/user.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { RoleModel } from '../../models/role.model';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule,UserPipe,FormsModule,FormValidateDirective,RouterLink],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent {
  users:UserModel[]=[];
  roles:RoleModel[]=[];
  createUserModel:UserModel=new UserModel();
  updateUserModel:UserModel=new UserModel();

  @ViewChild("addModalCloseButton") addModalCloseButton:ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("UpdateModalCloseButton")  UpdateModalCloseButton:ElementRef<HTMLButtonElement> | undefined

  ngOnInit(): void {
    this.getAll();
    this.GetAllRoles();
  }

search:string=""


constructor(

  private http:HttpService,
  private swal:SwalService
) {

  
}
 

getAll(){
  this.http.post<UserModel[]>("Users/GetAll",{},(res)=>{
    this.users=res.data;
    
  })
}

GetAllRoles(){
  this.http.post<RoleModel[]>("Users/GetAllRoles",{},(res)=>{
    this.roles=res.data;
    console.log(res.data)
  });
}


add(form:NgForm){
  if(form.valid){
    this.http.post<string>("Users/Create",this.createUserModel,(res)=>{
      this.swal.callToast(res.data,"success")

      this.getAll();
      this.addModalCloseButton?.nativeElement.click();
      this.createUserModel=new UserModel();
      
    })



  }

}

delete(id:string,fullName:string){
this.swal.callSwal("Delete",`You want to delete ${fullName}`,()=>{
this.http.post<string>("Users/Delete",{id:id},(res)=>{
  this.swal.callToast(res.data,"success")
  this.getAll();
})

})

}

get(data:UserModel){
this.updateUserModel={...data}

}


update(form:NgForm){
if(form.valid){
  this.http.post<string>("Users/Update",this.updateUserModel,(res)=>{
    console.log(res)
    this.swal.callToast(res.data,"success")
    this.getAll();
    this.UpdateModalCloseButton?.nativeElement.click();

  })
}

}
}

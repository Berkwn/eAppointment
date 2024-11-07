export class doctorModel{
    id:string="";
    firstName:string="";
    lastName:string="";
    fullName:string="";
    department:DepartmentModel=new DepartmentModel();
    departmentvalue:number=0;
}

export class DepartmentModel{
    name:string="";
    value:number=0;
}
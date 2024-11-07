import { Pipe, PipeTransform } from '@angular/core';
import { doctorModel } from '../models/doctor.model';

@Pipe({
  name: 'doctor',
  standalone: true
})
export class DoctorPipe implements PipeTransform {

  transform(value: doctorModel[], search:string): doctorModel[] {
    if(!search){
      return value;
    }

    return value.filter(x=>x.fullName.toLowerCase().includes(search.toLocaleLowerCase())
  || x.department.name.toLowerCase().includes(search.toLocaleLowerCase())
  )
  }

}

import { Pipe, PipeTransform } from '@angular/core';
import { PatientModel } from '../models/patient.model';

@Pipe({
  name: 'patient',
  standalone: true
})
export class PatientPipe implements PipeTransform {

  transform(value:PatientModel[],search:string) {
    if(!search){
      return value;
    }

    return value.filter(x=>x.fullName.toLowerCase().includes(search.toLocaleLowerCase())
  || x.identityNumber.toLowerCase().includes(search.toLowerCase())
  || x.city.toLowerCase().includes(search.toLocaleLowerCase())) 
  
  }

}

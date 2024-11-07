import { Pipe, PipeTransform } from '@angular/core';
import { UserModel } from '../models/user.model';

@Pipe({
  name: 'user',
  standalone: true
})
export class UserPipe implements PipeTransform {

  transform(value: UserModel[], search: string): UserModel[] {
   if(!search){
    return value;
   }
   return value.filter(x=>
    x.firstName.toLocaleLowerCase().includes(search.toLowerCase()) ||
    x.email.toLocaleLowerCase().includes(search.toLowerCase()) ||
    x.userName.toLocaleLowerCase().includes(search.toLowerCase())
  );
  }

}

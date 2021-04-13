import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// user created imports
import { DepartmentComponent } from './department/department.component';
import { EmployeeComponent } from './employee/employee.component';

// add the route for the components here!!
// for example,
// if you want to go to "employee", the path would be 'employee' and the component is Employee component

const routes: Routes = [
  {path: 'employee', component: EmployeeComponent},
  {path: 'department', component: DepartmentComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

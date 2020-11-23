import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { DepartmentComponent } from './department/department.component';
import { EmployeeComponent } from './employee/employee.component';

const routes: Routes = [
                         { path:'department',component:DepartmentComponent },
                         { path:'employee',component:EmployeeComponent }
                      ];

@NgModule({
  imports: [RouterModule.forRoot(routes),HttpClientModule],
  exports: [RouterModule]
})

export class AppRoutingModule { }

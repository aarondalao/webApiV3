// initial imports
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

// component imports needed for the app
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DepartmentComponent } from './department/department.component';
import { ShowDeptComponent } from './department/show-dept/show-dept.component';
import { AddEditDeptComponent } from './department/add-edit-dept/add-edit-dept.component';
import { EmployeeComponent } from './employee/employee.component';
import { ShowEmpComponent } from './employee/show-emp/show-emp.component';
import { AddEditEmpComponent } from './employee/add-edit-emp/add-edit-emp.component';
// service needed
import { SharedService } from './shared.service';

// register the HTTP client Module in app.module
import { HttpClientModule } from '@angular/common/http';

// import other form related modules
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialDesignModule } from './material-design/material-design.module';
import { MyDashboardSchematicComponent } from './my-dashboard-schematic/my-dashboard-schematic.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { LayoutModule } from '@angular/cdk/layout';
import { MyNavigationSchematicComponent } from './my-navigation-schematic/my-navigation-schematic.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MAT_DIALOG_DEFAULT_OPTIONS, MatDialogConfig } from '@angular/material/dialog';


@NgModule({
  declarations: [
    AppComponent,
    DepartmentComponent,
    ShowDeptComponent,
    AddEditDeptComponent,
    EmployeeComponent,
    ShowEmpComponent,
    AddEditEmpComponent,
    MyDashboardSchematicComponent,
    MyNavigationSchematicComponent
  ],
  entryComponents: [
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialDesignModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    LayoutModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    NgbModule
  ],
  providers: [
  {
    provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {
    disableClose: true, autoFocus: true, hasBackdrop: true, minHeight: '50%', 
    minWidth: '50%', maxHeight: '800px', maxWidth: '1200px'
  }},
  SharedService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

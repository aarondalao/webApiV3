// 1) import the shared service to use the written API methods
import { SharedService } from './../../shared.service';
import { Component, OnInit } from '@angular/core';
import { MaterialDesignModule } from './../../material-design/material-design.module';
import { AddEditDeptComponent } from './../add-edit-dept/add-edit-dept.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';



@Component({
  selector: 'app-show-dept',
  templateUrl: './show-dept.component.html',
  styleUrls: ['./show-dept.component.css']
})
export class ShowDeptComponent implements OnInit {

  // 2) instantiate the shared service in the constructor
  constructor(private service: SharedService, public dialog: MatDialog) { }

  // 3) assign an empty array for now
  DepartmentList: any = [];

  // variables for the dialog
  modalTitle: string;
  activateAddEditComp: boolean = false;
  dep: any;

  departmentid: any;
  departmentname: string;
  displayedColumns: string[] = ['departmentid', 'departmentname', 'actionbuttons'];

  ngOnInit() {
    // 5) also need to call refresh method to the ng init as this is the first executed code on show-dept-component.ts
    this.refreshDepList();
  }

  // 4) write a method to refresh the department list from the API method
  // note:
  // This method is using asynchronous operation. RESEARCH MORE ABOUT THIS
  // the subscribe method makes sure to wait until the response is received from API call and
  // then only assigns a value to the department list variable

  refreshDepList() {
    this.service.getDepList()
      .subscribe((data) => (
        this.DepartmentList = data,
        console.log(this.DepartmentList)
      )
    );
  }


  // method adding department to the dialog box
  addDepartment() {

    // swap this lines of code to a default Material Dialog Configurations in order to strictly pass the data from line 58
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.hasBackdrop = true;
    dialogConfig.data = {
      departmentid: 0,
      departmentname: '',
      modalTitle: 'Add Department'
    };
    const dialogRef = this.dialog.open(AddEditDeptComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      console.log('dialog result:', result);
    });
    this.refreshDepList();
  }

  editDepartment(dItem1,dItem2){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.hasBackdrop = true;
    dialogConfig.data = {
      departmentid: dItem1,
      departmentname: dItem2,
      modalTitle: 'Edit Department'
    };

    const dialogRef = this.dialog.open(AddEditDeptComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      console.log('dialog result:', result);
    });
    this.refreshDepList();
  }
  deleteDepartment(myItem){
    if(confirm('Are you sure?')){
      this.service.deleteDepartment(myItem.departmentid).subscribe(data =>{
        alert(data.toString());
      });
      this.refreshDepList();
    }
  }
}


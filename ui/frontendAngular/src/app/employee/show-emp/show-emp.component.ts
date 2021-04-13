import { AddEditEmpComponent } from './../add-edit-emp/add-edit-emp.component';
import { MaterialDesignModule } from './../../material-design/material-design.module';
import { SharedService } from './../../shared.service';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  constructor(private service: SharedService, public dialog: MatDialog) { }

  EmployeeList: any = [];
  displayedColumns: string[] = ['employeeid', 'employeename', 'department', 'dateofjoining', 'photofilename', 'actionbuttons'];

  modalTitle: string;

  employeeid: any;
  employeename: string;
  department: string;
  dateofjoining: string;
  photofilename: string;

  ngOnInit(): void {
    this.refreshEmpList();
  }


  refreshEmpList(){
    this.service.getEmpList().subscribe(data => {this.EmployeeList = data; });
  }

  addEmployee(){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.hasBackdrop = true;
    dialogConfig.data = {
      modalTitle: 'Add Employee',
      employeeid: 0,
      employeename: '',
      department: '',
      dateofjoining: '',
      photofilename: ''
    };
    const dialogRef =  this.dialog.open(AddEditEmpComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => { console.log('dialog result:', result); });
    this.refreshEmpList();
  }

  updateEmployee(myItem){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.hasBackdrop = true;
    dialogConfig.data = {
      modalTitle: 'Update Employee',
      employeeid: myItem.employeeid,
      employeename: myItem.employeename,
      department: myItem.department,
      dateofjoining: myItem.dateofjoining,
      photofilename: myItem.photofilename
    };
    const dialogRef =  this.dialog.open(AddEditEmpComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => { console.log('dialog result:', result); });
    this.refreshEmpList();
  }

  deleteEmployee(myItem){
    if(confirm('Are you sure though?')){
      this.service.deleteEmployee(myItem.employeeid).subscribe(data => {
        alert(data.toString());
      });
      this.refreshEmpList();
    }

  }
}

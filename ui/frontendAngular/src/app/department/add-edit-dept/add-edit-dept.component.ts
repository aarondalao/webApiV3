import { MaterialDesignModule } from './../../material-design/material-design.module';
import { ShowDeptComponent } from './../show-dept/show-dept.component';
import { SharedService } from './../../shared.service';
import { Component, OnInit, Inject, Input } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';



@Component({
  selector: 'app-add-edit-dept',
  templateUrl: './add-edit-dept.component.html',
  styleUrls: ['./add-edit-dept.component.css']
})
export class AddEditDeptComponent implements OnInit {

  // variables for the dialog
  modalTitle: string;
  departmentid: any;
  departmentname: string;
  designator: string;
  @Input() dep: any;

  // additional 21/03/2021
  form: FormGroup;



  constructor(private dialogRef: MatDialogRef<ShowDeptComponent>,
              @Inject(MAT_DIALOG_DATA) public data,
              private service: SharedService,
              private fb: FormBuilder) {
    this.modalTitle = data.modalTitle;
    this.designator = data.designator;
    this.departmentid = data.departmentid;
    this.departmentname = data.departmentname;
    // this.form = fb.group({
    //   departmentname: [this.departmentname, Validators.required]
    // });
  }

  ngOnInit(): void {
    this.departmentid = this.dep.departmentid;
    this.departmentname = this.dep.departmentname;
  }

  addDepartment() {
    var val = {
      departmentid: this.departmentid,
      departmentname: this.departmentname
    };
    console.log(this.departmentid);
    console.log(this.departmentname);
    this.service.addDepartment(val).subscribe(res => {
      alert(res.toString());
    });

    this.dialogRef.close();
  }

  updateDepartment() {
    var val = {
      departmentid: this.departmentid,
      departmentname: this.departmentname
    };
    this.service.updateDepartment(val).subscribe(res => {
      alert(res.toString());
    });
    this.dialogRef.close();
  }


  close() {
    this.dialogRef.close();
  }
}

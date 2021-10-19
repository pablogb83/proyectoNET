import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DialogData } from '../user-list/user-list.component';

@Component({
  selector: 'app-user-role',
  templateUrl: './user-role.component.html',
  styleUrls: ['./user-role.component.css']
})
export class UserRoleComponent implements OnInit {

  user: any;

  constructor(public dialogRef: MatDialogRef<UserRoleComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData) {
    this.user = data.user;
   }

  ngOnInit() {
  }

}

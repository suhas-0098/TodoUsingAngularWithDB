import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ServiceService } from './service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'demo';
  students: any;

  formHeader = 'Add TODO';
  StudentName = '';
  Studentid = -1;
  StudentDescription = '';

  showForm = false;

  editMode = false;

  constructor(private http: HttpClient, private ss: ServiceService) {}
  ngOnInit(): void {
    this.getStudents();
  }

  getStudents() {
    this.ss.fetchStudent().subscribe(
      (data) => {
        this.students = data;
        console.log(data);
      },
      (error) => {
        console.log('error');
      }
    );
  }

  DeleteStudent(id: number) {
    this.ss.deleteStudent(id).subscribe((res) => {
      this.getStudents();
    });
  }

  openForm(data: any) {
    this.clearForm();
    this.showForm = true;

    if (data) {
      this.StudentName = data.name;
      this.Studentid = data.id;
      this.StudentDescription = data.description1;
      this.formHeader = 'Edit TODO';
      this.editMode = true;
    } else {
      this.editMode = false;
      this.formHeader = 'Add TODO';
    }
  }

  clearForm() {
    this.StudentName = '';
    this.StudentDescription = '';
    this.Studentid = -1;
  }

  closeForm() {
    this.showForm = false;
    this.clearForm();
  }

  saveStudent() {
    this.showForm = false;

    let body = {
      name: this.StudentName,
      id: this.Studentid,
      description1: this.StudentDescription,
    };

    if (this.editMode) {
      body['id'] = this.Studentid;
      const val = this.Studentid;

      this.ss.putStudent(body).subscribe((res) => {
        this.getStudents();
      });
    } else {
      console.log(this.StudentDescription);
      this.ss.postStudent(body).subscribe((res) => {
        this.getStudents();
      });
    }
  }
}

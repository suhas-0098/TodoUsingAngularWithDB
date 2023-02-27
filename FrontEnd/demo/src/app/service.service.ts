import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ServiceService {
  constructor(private http: HttpClient) {}

  url = 'https://localhost:7182/';

  fetchStudent() {
    return this.http.get(this.url + 'GetAllStudents');
  }

  deleteStudent(id: number) {
    return this.http.delete(this.url + '' + id);
  }

  postStudent(body: any) {
    return this.http.post(this.url + 'AddStudents', body);
  }

  putStudent(body: any) {
    return this.http.put(this.url + '' + body.id, body);
  }
}

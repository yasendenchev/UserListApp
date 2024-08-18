import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { PagedUsers } from '../models/paged-users.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private usersPath = '/api/users';

  constructor(private http: HttpClient) { }

  getAllUsers(searchQuery?: string, pageNumber?: number, pageSize?: number): Observable<PagedUsers> {
    let params: any = this.buildGetAllUsersParams(searchQuery, pageNumber, pageSize);
    return this.http.get<PagedUsers>(this.usersPath, { params });
  }

  addUser(user: User): Observable<any> {
    return this.http.post(`${this.usersPath}`, user);
  }

  updateUser(id: number, user: User): Observable<void> {
    return this.http.put<void>(`${this.usersPath}/${id}`, user);
  }

  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`${this.usersPath}/${id}`);
  }

  private buildGetAllUsersParams(searchQuery?: string, pageNumber?: number, pageSize?: number) {
    let params: any = {};
    if (searchQuery && searchQuery.length) {
      params.queryNames = searchQuery.split(',').map(name => name.trim());
    }
    if (pageNumber !== undefined && pageNumber !== null) {
      params.pageNumber = pageNumber.toString();
    }
    if (pageSize !== undefined && pageSize !== null) {
      params.pageSize = pageSize.toString();
    }

    return params;
  }
}

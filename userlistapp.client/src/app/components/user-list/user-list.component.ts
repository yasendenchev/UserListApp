import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { ToastrService } from 'ngx-toastr';
import { UserFormComponent } from '../user-form/user-form.component';
import { PagedUsers } from '../../models/paged-users.model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  searchQuery: string = '';
  pageNumber: number = 0;
  pageSize: number = 10;
  totalUsers: number = 0;
  @ViewChild(UserFormComponent) userFormComponent!: UserFormComponent;

  constructor(private userService: UserService, private toastr: ToastrService) { }

  ngOnInit() {
    this.loadUsers();
  }

  ngAfterViewInit() {
    if (this.userFormComponent) {
      this.userFormComponent.formClosed.subscribe(() => {
        this.loadUsers();
      });
    }
  }

  loadUsers() {
    this.userService.getAllUsers(this.searchQuery, this.pageNumber, this.pageSize).subscribe(
      {
        next: (response: PagedUsers) => {
          this.users = response.users;
          this.totalUsers = response.totalCount;
        },
        error: (error) => {
          this.toastr.error(error, 'Failed to load users');
        }
      }
    );
  }

  onPageSizeChange() {
    this.reloadUsers();
  }

  search() {
    this.reloadUsers();
  }

  openUserForm(isEditMode: boolean, user?: User): void {
    this.userFormComponent!.openModal(isEditMode, user);
  }

  deleteUser(id: number) {
    this.userService.deleteUser(id).subscribe({
      next: () => {
        this.toastr.success('User deleted successfully');
        if (this.users.length === 1) {
          this.onPageChange(this.pageNumber - 1);
        }
        else {
          this.loadUsers();
        }
      },
      error: (error) => {
        this.toastr.error(error, 'Failed to delete user');
      }
    })
  }

  onPageChange(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.loadUsers();
  }

  private reloadUsers() {
    this.pageNumber = 0;
    this.loadUsers();
  }
}
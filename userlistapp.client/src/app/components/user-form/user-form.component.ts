import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {
  userForm: FormGroup;
  isEditMode: boolean = false;
  isVisible: boolean = false;
  userId?: number;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private toastr: ToastrService
  ) {
    this.userForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  openModal(isEditMode: boolean, user?: User): void {
    this.isEditMode = isEditMode;
    if (user) {
      this.userId = user.id;
      this.userForm.patchValue(user);
    } else {
      this.userForm.reset();
      this.userId = undefined;
    }
    this.isVisible = true;
  }

  closeModal(): void {
    this.isVisible = false;
  }

  submit() {
    if (this.userForm.invalid) {
      this.toastr.error('All fields are required', 'Validation Error');
      return;
    }

    const user: User = this.userForm.value;

    if (this.isEditMode) {
      this.userService.updateUser(this.userId!, user).subscribe(
        () => {
          this.toastr.success('User updated successfully');
          this.userForm.reset();
          this.isEditMode = false;
        },
        (error) => {
          this.toastr.error(error, 'Failed to update user');
        }
      );
    } else {
      this.userService.addUser(user).subscribe(
        () => {
          this.toastr.success('User created successfully');
          this.userForm.reset();
        },
        (error) => {
          this.toastr.error(error, 'Failed to create user');
        }
      );
    }
  }

  loadUser(user: User) {
    this.isEditMode = true;
    this.userId = user.id;
    this.userForm.patchValue(user);
  }
}

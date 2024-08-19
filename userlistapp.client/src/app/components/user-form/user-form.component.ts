import { Component, EventEmitter, OnInit, Output } from '@angular/core';
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

  @Output() formClosed: EventEmitter<void> = new EventEmitter<void>();

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private toastr: ToastrService
  ) {
    this.userForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

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
    this.formClosed.emit();
  }

  submit() {
    if (this.userForm.invalid) {
      this.toastr.error('All fields are required', 'Validation Error');
      return;
    }

    const user: User = this.userForm.value;

    if (this.isEditMode) {
      user.id = this.userId;
      this.userService.updateUser(this.userId!, user).subscribe(
        {
          next: () => {
            this.toastr.success('User updated successfully');
            this.closeModal()
          },
          error: (error) => this.displayErrorMessages(error)
        }
      );
    } else {
      this.userService.addUser(user).subscribe(
        {
          next: () => {
            this.toastr.success('User created successfully');
            this.userForm.reset();
            this.closeModal();
          },
          error: (error) => this.displayErrorMessages(error)
        }
      );
    }
  }

  loadUser(user: User) {
    this.isEditMode = true;
    this.userId = user.id;
    this.userForm.patchValue(user);
  }

  private displayErrorMessages(error: any) {
    let errors = Object.values(error.error.errors).flatMap(arr => arr).join('<br/>');
    this.toastr.error(errors, 'Failed to create user:');
  }
}

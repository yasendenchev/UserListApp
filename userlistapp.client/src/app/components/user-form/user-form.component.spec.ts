import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { UserFormComponent } from './user-form.component';
import { of, throwError } from 'rxjs';

describe('UserFormComponent', () => {
    let component: UserFormComponent;
    let fixture: ComponentFixture<UserFormComponent>;
    let mockUserService: jasmine.SpyObj<UserService>;
    let mockToastrService: jasmine.SpyObj<ToastrService>;

    beforeEach(async () => {
        mockUserService = jasmine.createSpyObj('UserService', ['addUser', 'updateUser']);
        mockToastrService = jasmine.createSpyObj('ToastrService', ['error', 'success']);

        await TestBed.configureTestingModule({
            imports: [ReactiveFormsModule, FormsModule],
            declarations: [UserFormComponent],
            providers: [
                FormBuilder,
                { provide: UserService, useValue: mockUserService },
                { provide: ToastrService, useValue: mockToastrService }
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(UserFormComponent);
        component = fixture.componentInstance;
    });

    beforeEach(() => {
        component.ngOnInit();

        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should initialize form with empty values', () => {
        expect(component.userForm.value).toEqual({
            name: '',
            email: '',
            phoneNumber: ''
        });
    });

    describe('openModal', () => {
        it('should open modal in create mode', () => {
            component.openModal(false);

            expect(component.isEditMode).toBeFalse();
            expect(component.userId).toBeUndefined();
            expect(component.isVisible).toBeTrue();
        });

        it('should open modal in edit mode and populate form with user data', () => {
            const user =
            {
                id: 1,
                name: 'Bruno Fernandes',
                email: 'bruno@mutd.com',
                phoneNumber: '1234567890'
            };
            component.ngOnInit();
            component.openModal(true, user);
            fixture.detectChanges();

            expect(component.userForm.value).toEqual(
                {
                    name: 'Bruno Fernandes',
                    email: 'bruno@mutd.com',
                    phoneNumber: '1234567890'
                });

            expect(component.isEditMode).toBeTrue();
            expect(component.userId).toBe(1);
            expect(component.isVisible).toBeTrue();
        });
    });

    describe('closeModal', () => {
        it('should close modal and emit formClosed event', () => {
            spyOn(component.formClosed, 'emit');

            component.closeModal();
            expect(component.isVisible).toBeFalse();
            expect(component.formClosed.emit).toHaveBeenCalled();
        });
    });

    describe('submit', () => {
        it('should show error message when form is invalid and submitted', () => {
            component.submit();

            expect(mockToastrService.error).toHaveBeenCalledWith('All fields are required', 'Validation Error');
        });
        it('should call addUser and show success message on form submission (create mode)', () => {
            const userForm = {
                name: 'John Doe',
                email: 'john@example.com',
                phoneNumber: '1234567890'
            }
            component.userForm.setValue(userForm);

            mockUserService.addUser.and.returnValue(of(void 0));

            component.submit();

            expect(mockUserService.addUser).toHaveBeenCalledWith(userForm);
            expect(mockToastrService.success).toHaveBeenCalledWith('User created successfully');
        });
        it('should call updateUser and show success message on form submission (edit mode)', () => {
            mockUserService.updateUser.and.returnValue(of(void 0));

            component.isEditMode = true;
            component.userId = 1;
            component.userForm.setValue({ name: 'John Doe', email: 'john@example.com', phoneNumber: '1234567890' });

            component.submit();

            expect(mockUserService.updateUser).toHaveBeenCalledWith(1, component.userForm.value);
            expect(mockToastrService.success).toHaveBeenCalledWith('User updated successfully');
        });
        it('should handle and display error messages from the service', () => {
            mockUserService.addUser.and.returnValue(throwError({ error: { errors: { email: ['Invalid email'] } } }));

            component.userForm.setValue({ name: 'John Doe', email: 'john@example.com', phoneNumber: '1234567890' });
            component.submit();
            expect(mockToastrService.error).toHaveBeenCalledWith('Invalid email', 'Failed to create user:');
        });
    });
});

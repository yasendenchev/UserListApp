/* tslint:disable:no-unused-variable */
import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';

import { UserListComponent } from './user-list.component';
import { UserService } from '../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { UserFormComponent } from '../user-form/user-form.component';
import { of, throwError } from 'rxjs';
import { PagedUsers } from '../../models/paged-users.model';

describe('UserListComponent', () => {
  let component: UserListComponent;
  let fixture: ComponentFixture<UserListComponent>;
  let mockUserService: jasmine.SpyObj<UserService>;
  let mockToastrService: jasmine.SpyObj<ToastrService>;

  beforeEach(async () => {
    mockUserService = jasmine.createSpyObj('UserService', ['getAllUsers', 'deleteUser']);
    mockToastrService = jasmine.createSpyObj('ToastrService', ['error', 'success']);

    await TestBed.configureTestingModule({
      declarations: [UserListComponent, UserFormComponent],
      providers: [
        { provide: UserService, useValue: mockUserService },
        { provide: ToastrService, useValue: mockToastrService },
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(UserListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('loadUsers', () => {
    let mockPagedUsers: PagedUsers;

    beforeEach(() => {
      mockPagedUsers = {
        users: [
          {
            id: 1,
            name: 'James Rodriguez',
            email: 'james@email.com',
            phoneNumber: '1111111111'
          },
          {
            id: 2,
            name: 'Roberto Carlos',
            email: 'carlos@email.com',
            phoneNumber: '2222222222'
          }],
        totalCount: 2
      };
    });

    it('should load users and update users and totalUsers on success', () => {
      mockUserService.getAllUsers.and.returnValue(of(mockPagedUsers));

      component.loadUsers();

      expect(component.users).toEqual(mockPagedUsers.users);
      expect(component.totalUsers).toBe(mockPagedUsers.totalCount);
    });

    it('should call service.getAllUsers() users with correct parameter values', () => {
      
      mockUserService.getAllUsers.and.returnValue(of());
      
      const searchQuery = "s";
      const pageNumber = 2;
      const pageSize = 2;

      component.searchQuery = searchQuery;
      component.pageNumber = pageNumber;
      component.pageSize = pageSize;
      
      component.loadUsers();
  
      expect(mockUserService.getAllUsers).toHaveBeenCalledWith(searchQuery, pageNumber, pageSize);
    });


    it('should show an error toast when loadUsers fails', () => {
      const errorResponse = 'Fail';
      mockUserService.getAllUsers.and.returnValue(throwError(() => errorResponse));

      component.loadUsers();

      expect(mockToastrService.error).toHaveBeenCalledWith(errorResponse, 'Failed to load users');
    });
  });

  describe('search', () => {
    it('should reset pageNumber to 0 and call loadUsers', () => {
      spyOn(component, 'loadUsers');
      
      component.pageNumber = 10;
      component.search();
      
      expect(component.pageNumber).toBe(0);
      expect(component.loadUsers).toHaveBeenCalled();
    });
  });

  describe('onPageSizeChange', () => {
    it('should reset pageNumber to 0 and call loadUsers', () => {
      spyOn(component, 'loadUsers');
      
      component.pageNumber = 100;
      component.search();
      
      expect(component.pageNumber).toBe(0);
      expect(component.loadUsers).toHaveBeenCalled();
    });
  });

  describe('deleteUser', () => {
    it('should show a success message and reload the users on success', () => {
      spyOn(component, 'loadUsers');

      mockUserService.deleteUser.and.returnValue(of(void 0));

      component.deleteUser(1);

      expect(mockToastrService.success).toHaveBeenCalledWith('User deleted successfully');
      expect(component.loadUsers).toHaveBeenCalled();
    });
    it('should stay on the same page if there are any users left on the current page after deletion', () => {
      spyOn(component, 'loadUsers');

      component.pageSize = 2;
      component.pageNumber = 2;
      component.users = [{
        id: 1,
        name: 'James Rodriguez',
        email: 'james@email.com',
        phoneNumber: '1111111111'
      },
      {
        id: 2,
        name: 'Roberto Carlos',
        email: 'carlos@email.com',
        phoneNumber: '2222222222'
      }];

      mockUserService.deleteUser.and.returnValue(of(void 0));

      component.deleteUser(1);
      
      expect(component.pageNumber).toEqual(2);
    });

    it('should set pageNumber to the previous page if there are no users left on the current page after deletion', () => {
      spyOn(component, 'loadUsers');

      component.pageSize = 2;
      component.pageNumber = 10;
      component.users = [{
        id: 1,
        name: 'James Rodriguez',
        email: 'james@email.com',
        phoneNumber: '1111111111'
      }];

      mockUserService.deleteUser.and.returnValue(of(void 0));

      component.deleteUser(1);
      
      expect(component.pageNumber).toEqual(9);
    });

    it('should call onPageChange with the previous page\'s number value if there are no users left on the current page after deletion', () => {
      spyOn(component, 'loadUsers');
      spyOn(component, 'onPageChange');

      component.pageSize = 2;
      component.pageNumber = 10;
      component.users = [{
        id: 1,
        name: 'James Rodriguez',
        email: 'james@email.com',
        phoneNumber: '1111111111'
      }];

      mockUserService.deleteUser.and.returnValue(of(void 0));

      component.deleteUser(1);
      
      expect(component.onPageChange).toHaveBeenCalledWith(9);
    });
  })
});

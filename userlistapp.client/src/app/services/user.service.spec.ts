import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UserService } from './user.service';
import { User } from '../models/user.model';
import { PagedUsers } from '../models/paged-users.model';

describe('UserService', () => {
  let service: UserService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [UserService]
    });

    service = TestBed.inject(UserService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getAllUsers', () => {
    it('should return an Observable<PagedUsers>', () => {
      const dummyUsers: PagedUsers = {
        users: [{ id: 1, name: 'Michael', email: 'carrick@mutd.com', phoneNumber: '1234567890' }],
        totalCount: 1
      };

      service.getAllUsers().subscribe(users => {
        expect(users).toEqual(dummyUsers);
      });

      const req = httpMock.expectOne('/api/users');
      expect(req.request.method).toBe('GET');
      req.flush(dummyUsers);
    });

    it('should include search query params when provided', () => {
      service.getAllUsers('Onana,Aaron', 1, 10).subscribe();

      const req = httpMock.expectOne(request => request.url === '/api/users');
      expect(req.request.params.getAll('queryNames')).toEqual(['Onana', 'Aaron']);
      expect(req.request.params.get('pageNumber')).toBe('1');
      expect(req.request.params.get('pageSize')).toBe('10');
    });
  });

  describe('addUser', () => {
    it('should send a POST request', () => {
      const dummyUser: User = { name: 'Robert Lewandowski', email: 'lewangoalski@example.com', phoneNumber: '1234567890' };

      service.addUser(dummyUser).subscribe();

      const req = httpMock.expectOne('/api/users');
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(dummyUser);
    });
  });

  describe('updateUser', () => {
    it('should send a PUT request', () => {
      const dummyUser: User = { id: 1, name: 'Kobbie Mainoo', email: 'kobbie@mutd.com', phoneNumber: '1234567890' };

      service.updateUser(1, dummyUser).subscribe();

      const req = httpMock.expectOne('/api/users/1');
      expect(req.request.method).toBe('PUT');
      expect(req.request.body).toEqual(dummyUser);
    });
  });

  describe('deleteUser', () => {
    it('should send a DELETE request', () => {
      service.deleteUser(1).subscribe();

      const req = httpMock.expectOne('/api/users/1');
      expect(req.request.method).toBe('DELETE');
    });
  });

  describe('buildGetAllUsersParams', () => {
    it('should build correct params object', () => {
      const params = (service as any).buildGetAllUsersParams('John,Jane', 1, 10);

      expect(params).toEqual({
        queryNames: ['John', 'Jane'],
        pageNumber: '1',
        pageSize: '10'
      });
    });

    it('should handle empty search query', () => {
      const params = (service as any).buildGetAllUsersParams('', 1, 10);

      expect(params).toEqual({
        pageNumber: '1',
        pageSize: '10'
      });
    });

    it('should handle undefined pagination params', () => {
      const params = (service as any).buildGetAllUsersParams('John');

      expect(params).toEqual({
        queryNames: ['John']
      });
    });
  });
});
import { User } from "./user.model";

export interface PagedUsers {
  users: User[],
  totalCount: number
}

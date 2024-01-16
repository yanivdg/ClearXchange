
export enum Gender {
  '',
  Male,
  Female,
  Other
}
export interface MemberObj {
  Id: string;
  Name: string;
  Email: string;
  DateOfBirth: string;
  Gender: Gender;
  Phone: string;
}

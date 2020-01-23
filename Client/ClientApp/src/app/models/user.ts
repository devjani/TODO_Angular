import { RoleType } from './common';

export class User {
    UserNo: string;
    Name: string;
    Email: string;
    Password: string;
    Id: string;
}

export class LoggedInUserDetails {
    email: string;
    token: string;
    name: string;
    id: string;
    refreshToken: string;
}



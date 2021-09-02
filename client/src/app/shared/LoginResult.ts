export class LoginResult {
    token: string = '';
    expiration: Date = new Date();
}

export class LoginRequest {
    username: string = '';
    password: string = '';
}
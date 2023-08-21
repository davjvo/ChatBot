export class SignInDto {
  public DisplayName: string;
  public Password: string;

  constructor() {
    this.DisplayName = '';
    this.Password = '';
  }
}
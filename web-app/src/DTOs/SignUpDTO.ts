export class SignUpDTO {
  public DisplayName: string;
  public FirstName: string;
  public LastName: string;
  public Email: string;
  public Password: string;
  public PasswordConfirm: string;

  constructor() {
    this.DisplayName = '';
    this.FirstName = '';
    this.LastName = '';
    this.Email = '';
    this.Password = '';
    this.PasswordConfirm = '';
  }
}
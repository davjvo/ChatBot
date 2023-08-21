export class PostMessageDTO {
  public Message: string;
  public User: number;

  constructor() {
    this.Message = '';
    this.User = 0;
  }
}
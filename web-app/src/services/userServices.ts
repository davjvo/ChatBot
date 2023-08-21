import { SignInDto } from "../DTOs/SignInDTO";
import { SignUpDTO } from "../DTOs/SignUpDTO";
import { SignUpDtoResponse } from "../DTOs/SignUpDtoResponse";
import { Constants } from "../utils/constants";

export const UserService = {
  signIn: async (signInDto: SignInDto) => {
    const response = await fetch(`${Constants.API_BASE_URL}User/SignIn`, {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(signInDto),
    });

    if (!response.ok) {
      throw new Error('Sign In failed');
    }

    const data = await response.json();
    return data;
  },
  signUp: async (signUpDto: SignUpDTO): Promise<SignUpDtoResponse> => {
    const response = await fetch(`${Constants.API_BASE_URL}User/SignUp`, {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(signUpDto),
    });

    if (!response.ok) {
      throw new Error('Sign In failed');
    }

    const data = await response.json();
    return data;
  }
}
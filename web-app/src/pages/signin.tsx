import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import { Link, useNavigate } from 'react-router-dom';
import { SignInDto } from '../DTOs/SignInDTO';
import helpers from '../utils/helpers';
import { UserService } from '../services/userServices';
import { Alert, CircularProgress, IconButton, Snackbar } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

export const SignIn: React.FC = () => {
  const [isLoading, setIsLoading] = React.useState(false);
  const [isOpen, setIsOpen] = React.useState(false);
  const [error, setError] = React.useState("");
  const navigate = useNavigate();

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    const signInDto = new SignInDto();
    signInDto.DisplayName = helpers.getValueOrDefault(data, 'username');
    signInDto.Password = helpers.getValueOrDefault(data, 'password');

    try {
      setIsLoading(true);
      var singInResult = await UserService.signIn(signInDto);
      if (singInResult && singInResult.success) {
        navigate('./chat', {
          state: {
            user: signInDto.DisplayName,
            userId: singInResult.userId
          }
        });
      }
    }
    catch (exception) {
      let message = '';

      if (exception instanceof Error)
        message = exception?.message;
      else if (typeof exception === "string")
        message = exception;

      setError(`${message} Check that you've correctly set the constants under the utils folder.`);
      setIsOpen(true);
    }
    finally {
      setIsLoading(false);
    }
  };

  const handleToastClose = () => {
    setIsOpen(false);
  }

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <Snackbar open={isOpen} autoHideDuration={1500}
        onClose={handleToastClose} anchorOrigin={{
          horizontal: 'right',
          vertical: 'top'
        }} action={
          <IconButton
            size="small"
            aria-label="close"
            color="inherit"
            onClick={handleToastClose}
          >
            <CloseIcon fontSize="small" />
          </IconButton>}>
        <Alert severity="error" sx={{ width: '100%' }}>
          {error}
        </Alert>
      </Snackbar>
      <Box
        sx={{
          marginTop: 8,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }} >
        <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Sign in
        </Typography>
        <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            id="username"
            label="Username"
            name="username"
            autoComplete="username"
            autoFocus />
          <TextField
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type="password"
            id="password"
            autoComplete="current-password" />
          {
            !isLoading ?
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }} >
                Sign In
              </Button> :
              <Grid sx={{
                display: 'flex',
                width: '100%'
              }} justifyContent='center' alignItems='center'>
                <CircularProgress />
              </Grid>
          }
          <Grid container justifyContent="flex-end">
            <Grid item>
              <Link to="/signup">
                Not have an account? Sign up
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}
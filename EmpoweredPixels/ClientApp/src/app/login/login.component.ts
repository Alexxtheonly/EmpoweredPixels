import { UserFeedbackService } from './../+services/userfeedback.service';
import { Login } from './../auth/+models/login';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from './../auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit
{
  public loginForm: FormGroup;
  public loading = false;
  public submitted = false;

  constructor(
    private auth: AuthService,
    private formBuilder: FormBuilder,
    private router: Router,
    private userfeedbackService: UserFeedbackService) { }

  ngOnInit()
  {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.auth.logout();
  }

  get f() { return this.loginForm.controls; }

  public submit(): void
  {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid)
    {
      return;
    }

    this.loading = true;

    const login = new Login();
    login.user = this.f.username.value;
    login.password = this.f.password.value;

    this.auth.login(login)
      .pipe(first())
      .subscribe(
        data =>
        {
          this.router.navigate(['/']);
        },
        error =>
        {
          this.userfeedbackService.error(error);
          this.loading = false;
        });
  }
}

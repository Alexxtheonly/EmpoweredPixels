import { TranslateService } from '@ngx-translate/core';
import { UserFeedbackService } from './../+services/userfeedback.service';
import { RegisterData } from './+modules/register-data';
import { RegisterService } from './+services/register.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit
{
  public registerForm: FormGroup;
  public loading = false;
  public submitted = false;

  constructor(
    private registerService: RegisterService,
    private userfeedbackService: UserFeedbackService,
    private formbuilder: FormBuilder,
    private router: Router,
    private translateService: TranslateService) { }

  ngOnInit()
  {
    this.registerForm = this.formbuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required, Validators.minLength(6)],
      repeatPassword: [''],
      email: ['', Validators.required, Validators.email],
      repeatEmail: [''],
    });
  }

  get f() { return this.registerForm.controls; }

  public register(): void
  {
    this.submitted = true;

    if (this.registerForm.invalid)
    {
      return;
    }

    this.loading = true;

    const data = new RegisterData();
    data.username = this.registerForm.controls.username.value;
    data.password = this.registerForm.controls.password.value;
    data.email = this.registerForm.controls.email.value;

    this.registerService.register(data).subscribe(() =>
    {
      this.userfeedbackService.success(this.translateService.instant('registerSuccessfull'));
      this.router.navigate(['login']);
    }, error =>
    {
      this.userfeedbackService.error(error);
      this.loading = false;
    });
  }
}

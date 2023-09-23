import { ResponseLoginUser } from './../models/response-login-user';
import { LoginUser } from './../models/login-user';
import { RegisterUser } from './../models/register-user';
import { AccountService } from './../services/account.service';
import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChildren } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { FormBaseComponent } from 'src/app/shared/components/form-base/form-base.component';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GenericValidator } from 'src/app/shared/utils/generic-form-validation';
import { CustomValidators } from '@narik/custom-validators';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent extends FormBaseComponent implements OnInit, AfterViewInit {
  PASSWORD: string = 'password';
  CONFIRM_PASSWORD: string = 'confirmPassword';

  //Obtem todos dados do DOM(HTML)
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements: ElementRef[] = [];

  errors: any[] = [];
  registerForm !: FormGroup;
  registerUser !: RegisterUser;
  loginUser !: LoginUser;
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private accountService: AccountService,
    private toastr: ToastrService) {

    super();

    this.validationMessages = {
      email: {
        required: 'Informe o e-mail',
        email: 'Email inválido'
      },
      password: {
        required: 'Informe a senha',
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres'
      },
      confirmPassword: {
        required: 'Informe a senha novamente',
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres',
        equalTo: 'As senhas não conferem',
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    let password = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15])]);
    let confirmPassword = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15]), CustomValidators.equalTo(password)]);

    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]], //''valores iniciais para o formulario
      password: password,
      confirmPassword: confirmPassword
    });
  }

  ngAfterViewInit(): void {
    super.configureBaseFormValidation(this.formInputElements, this.registerForm);
  }

  register(): void {
    if (this.registerForm.dirty && this.registerForm.valid) {
      //mapeando os valores para um objeto do tipo registerUser com valores do formulario
      this.registerUser = Object.assign({}, this.registerUser, this.registerForm.value);

      this.accountService.register(this.registerUser).subscribe({
        next: (responseRegisterUser) => {
          this.loginUser = {
            email: this.registerUser.email,
            password: this.registerUser.password
          };

          this.accountService.login(this.loginUser).subscribe({
            next: (responseLoginUser) => { this.processSuccess(responseLoginUser); },
            error: (error) => { this.processError(error); }
          });
        },
        error: (error) => { console.log("erro"); this.processError(error); }
      });
    }
  }

  processSuccess(responseLoginUser: ResponseLoginUser) {
    this.registerForm.reset();
    this.errors = [];

    console.log("user criado =>", responseLoginUser);
    this.toastr.success('Usuario criado com sucesso!', 'Bem Vindo!!!',
      {
        progressBar: true,
        closeButton: true,
        timeOut: 3000
      });

    this.accountService.LocalStorage.saveLocalUserData(responseLoginUser);
    this.router.navigate(['/home']);
  }

  processError(fail: HttpErrorResponse) {
    console.log('falha', fail);
    this.errors = fail.error?.errors;

    this.toastr.error('Algo deu errado!', 'Opa :(',
      {
        progressBar: true,
        closeButton: true,
        timeOut: 3000
      });
  }

  setPasswordVisibility(): void {
    this.showPassword = !this.showPassword
  }

  setConfirmPasswordVisibility(): void {
    this.showConfirmPassword = !this.showConfirmPassword
  }

  clearErrors(event: string[]): void {
    this.errors = event;
  }
}
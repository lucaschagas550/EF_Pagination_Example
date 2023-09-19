import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { LoginUser } from '../models/login-user';
import { GenericValidator, } from 'src/app/shared/utils/generic-form-validation';
import { ActivatedRoute, Router } from '@angular/router';

import { CustomValidators } from '@narik/custom-validators';
import { FormBaseComponent } from 'src/app/shared/components/form-base/form-base.component';
import { AccountService } from '../services/account.service';
import { ResponseLoginUser } from '../models/response-login-user';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent extends FormBaseComponent implements OnInit, AfterViewInit {
  //Obtem todos dados do DOM(HTML)
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements: ElementRef[] = [];

  errors: any[] = [];
  loginForm!: FormGroup; // FormGroup para a model do formulario e validar os campo do formulario
  loginUser!: LoginUser;
  returnUrl!: string;
  showPassword: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute, //Obter o valor do queryparam que tem url de retorno se acessado por redirecionamento
    private router: Router,
    private accountService: AccountService,
    private toastr: ToastrService) {

    super();

    this.validationMessages = {
      email: { required: 'Informe o e-mail', email: 'Email inválido' },
      password: { required: 'Informe a senha', rangeLength: 'A senha deve possuir entre 6 e 15 caracteres' },
    };

    console.log('route snapshot => ', this.route.snapshot.queryParams);
    this.returnUrl = this.route.snapshot.queryParams['returnUrl']; // Valor eh salvo no queryParams pelo error.interceptor
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]], //''valores iniciais para o formulario
      password: ['', [Validators.required, CustomValidators.rangeLength([6, 15])]]
    });
  }

  ngAfterViewInit(): void {
    super.configureBaseFormValidation(this.formInputElements, this.loginForm)
  }

  login(): void {
    if (this.loginForm.dirty && this.loginForm.valid) {
      //mapeando os valores para um objeto do tipo usuario com valores do formulario
      this.loginUser = Object.assign({}, this.loginUser, this.loginForm.value);
      console.log('user =>', this.loginUser);

      this.accountService.login(this.loginUser)
        .subscribe({
          next: (responseLoginUser) => { this.processSuccess(responseLoginUser); },
          error: (error) => { this.processError(error) },
        });
    }
  }

  processSuccess(responseLoginUser: ResponseLoginUser): void {
    this.loginForm.reset();
    this.errors = [];

    this.toastr.success('Login realizado com Sucesso!', 'Bem vindo!!!',
      {
        progressBar: true,
        closeButton: true,
        timeOut: 3000
      });

    this.accountService.LocalStorage.saveLocalUserData(responseLoginUser);
    this.returnUrl ? this.router.navigate([this.returnUrl]) : this.router.navigate(['/home']);
  }

  processError(fail: HttpErrorResponse) {
    console.log('falhaaa ', fail);
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

  clearErrors(event: string[]): void {
    this.errors = event;
  }
}

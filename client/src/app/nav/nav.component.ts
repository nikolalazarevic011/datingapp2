import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent {
  model: any = {};
  //! i ovo nam ne treba jer je lin 16 promenjena u public i moze dir u html da se zove
  // currentUser$: Observable<User | null> = of(null);

  //public da bi mogao u html strani da ga koristis
  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    //dir cemo da ga zovemo u html zbog lin 16 Public accservice
    // this.currentUser$ = this.accountService.currentUser$;
  }

  login() {
    this.accountService.login(this.model).subscribe({
      // next: (response) => {
      //   this.router.navigateByUrl('/members')
      // },
      //! vidjaces i ovako u kodu, a moze i da budu zagrade posle next(), umesto _
      next: (_) => this.router.navigateByUrl('/members'),
      error: (error) => this.toastr.error(error.error),
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('');
  }
}

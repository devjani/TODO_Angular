import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/core/services/login.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {

  constructor(
    private loginService: LoginService,
    private router: Router,
    private authService: AuthService,
  ) { }

  ngOnInit() {
    this.loginService.getToken().subscribe(response => {
      this.authService.setAuhToken('a');
      this.router.navigate(['']);
    });
  }

}

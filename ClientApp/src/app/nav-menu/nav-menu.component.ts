import { Component } from '@angular/core';
import { AuthService } from "../auth/auth.service"

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  authService: AuthService;

  constructor(authService: AuthService) {
    this.authService = authService;
  }
  hoge() {
    alert(this.authService.isLoggedIn);
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

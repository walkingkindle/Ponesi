import {AfterViewInit, Component, ElementRef, ViewChild} from '@angular/core';
import {RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})


export class NavbarComponent implements AfterViewInit {
  //@ts-ignore
  @ViewChild('mobileNavToggle') mobileNavToggleBtn: ElementRef;

  ngAfterViewInit() {
    this.mobileNavToggleBtn.nativeElement.addEventListener('click', this.mobileNavToggle.bind(this));
  }

  mobileNavToggle() {
    document.body.classList.toggle('mobile-nav-active');
    this.mobileNavToggleBtn.nativeElement.classList.toggle('bi-list');
    this.mobileNavToggleBtn.nativeElement.classList.toggle('bi-x');
  }
}



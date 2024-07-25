import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  private mobileNavToggleBtn: HTMLElement | null = null;

  ngOnInit() {
    this.mobileNavToggleBtn = document.querySelector('.mobile-nav-toggle');
    if (this.mobileNavToggleBtn) {
      this.mobileNavToggleBtn.addEventListener('click', this.mobileNavToggle.bind(this));
    }
  }

  private mobileNavToggle(): void {
    document.querySelector('body')?.classList.toggle('mobile-nav-active');
    this.mobileNavToggleBtn?.classList.toggle('bi-list');
    this.mobileNavToggleBtn?.classList.toggle('bi-x');
  }
}


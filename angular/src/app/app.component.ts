import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NavbarComponent} from "./constants/navbar/navbar.component";
import {FooterComponent} from "./constants/footer/footer.component";
import "@aarsteinmedia/dotlottie-player";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ponesi-frontend';
}

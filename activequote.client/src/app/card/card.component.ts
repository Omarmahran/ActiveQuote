import { Component, Input } from '@angular/core';
import {Quote} from '../app.component'


@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrl: './card.component.css'
})
export class CardComponent {
  @Input() quote!: Quote;  // Input to accept the Quote object
}

import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  http = inject(HttpClient);
  title = 'DatingApp';
  Users: any;

  ngOnInit(): void {
    this.http.get('http://localhost:5000/api/Users').subscribe({
      next: Response => this.Users = Response,
      error: Error => console.log(Error),
      complete: () => console.log('Request has completed')
    });
  }

}
